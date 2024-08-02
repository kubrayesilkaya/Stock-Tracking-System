using Capstone.Models.DBContext;
using Capstone.Models.Entities;
using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel;
using Capstone.Services.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IConfiguration configuration, IMemoryCache cache, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserEmail()
    {
        var emailClaim = _httpContextAccessor.HttpContext.User.Claims
                             .FirstOrDefault(c => c.Type == ClaimTypes.Email);

        return emailClaim?.Value;
    }

    #region token

    public string GenerateJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    #endregion

    #region login
    public AuthResponseModel Login(LoginRequestModel loginRequest)
    {
        using (var dbContext = new CAPSTONE_CONTEXT())
        {
            try
            {
                var cacheKey = $"{loginRequest.factoryType}_{loginRequest.email}";
                if (!_cache.TryGetValue(cacheKey, out string cachedToken))
                {
                    if (loginRequest.factoryType == "PRODUCT")
                    {
                        var productUser = dbContext.Set<FACTORY_PRODUCT>()
                                                   .FirstOrDefault(u => u.FACTORY_PRODUCT_EMAIL == loginRequest.email
                                                                     && u.FACTORY_PRODUCT_PASSWORD == loginRequest.password);

                        if (productUser != null)
                        {
                            var token = GenerateJwtToken(productUser.FACTORY_PRODUCT_EMAIL);
                            _cache.Set(cacheKey, token, TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])));
                            return new AuthResponseModel
                            {
                                successMessage = "Login successful!",
                                isSuccess = true,
                                Token = token
                            };
                        }
                    }
                    else if (loginRequest.factoryType == "DEALER")
                    {
                        var dealerUser = dbContext.Set<FACTORY_DEALER>()
                                                  .FirstOrDefault(u => u.FACTORY_DEALER_EMAIL == loginRequest.email
                                                                   && u.FACTORY_DEALER_PASSWORD == loginRequest.password);

                        if (dealerUser != null)
                        {
                            var token = GenerateJwtToken(dealerUser.FACTORY_DEALER_EMAIL);
                            _cache.Set(cacheKey, token, TimeSpan.FromMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])));
                            return new AuthResponseModel
                            {
                                successMessage = "Login successful!",
                                isSuccess = true,
                                Token = token
                            };
                        }
                    }
                }
                else
                {
                    return new AuthResponseModel
                    {
                        successMessage = "Login successful!",
                        isSuccess = true,
                        Token = cachedToken
                    };
                }

                return new AuthResponseModel { errorMessage = "Invalid email or password!", isSuccess = false };
            }
            catch (Exception ex)
            {
                return new AuthResponseModel { errorMessage = ex.Message, isSuccess = false };
            }
        }
    }
    #endregion

    #region signUp
    public AuthResponseModel Signup(SignupRequestModel signupRequest)
    {
        using (var dbContext = new CAPSTONE_CONTEXT())
        using (var dbContextTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                if (signupRequest.factoryType == "PRODUCT")
                {
                    var newProductUser = new FACTORY_PRODUCT
                    {
                        FACTORY_PRODUCT_EMAIL = signupRequest.email,
                        FACTORY_PRODUCT_PASSWORD = signupRequest.password,
                        FACTORY_PRODUCT_NAME = signupRequest.factoryName,
                    };

                    dbContext.Set<FACTORY_PRODUCT>().Add(newProductUser);
                }
                else if (signupRequest.factoryType == "DEALER")
                {
                    var newDealerUser = new FACTORY_DEALER
                    {
                        FACTORY_DEALER_EMAIL = signupRequest.email,
                        FACTORY_DEALER_PASSWORD = signupRequest.password,
                        FACTORY_DEALER_NAME = signupRequest.factoryName,
                    };

                    dbContext.Set<FACTORY_DEALER>().Add(newDealerUser);
                }
                else
                {
                    return new AuthResponseModel { errorMessage = "Geçersiz factory tipi!", isSuccess = false };
                }

                dbContext.SaveChanges();
                dbContextTransaction.Commit();
                return new AuthResponseModel { successMessage = "Kayıt başarılı!", isSuccess = true };
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return new AuthResponseModel { errorMessage = ex.Message, isSuccess = false };
            }
        }
    }
    #endregion
}
