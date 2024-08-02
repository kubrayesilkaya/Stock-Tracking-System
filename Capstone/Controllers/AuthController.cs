using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel;
using Capstone.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                if (loginRequest == null)
                {
                    return BadRequest(new AuthResponseModel { errorMessage = "Invalid login request.", isSuccess = false });
                }

                var response = _authService.Login(loginRequest);
                if (!string.IsNullOrEmpty(response.errorMessage))
                {
                    return Unauthorized(response);
                }

                // Baþarýlý giriþten sonra gerekli bilgileri döndür
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponseModel { errorMessage = ex.Message, isSuccess = false });
            }
        }


        [HttpPost("Signup")]
        public IActionResult Signup([FromBody] SignupRequestModel signupRequest)
        {
            if (signupRequest == null)
            {
                return BadRequest(new AuthResponseModel { errorMessage = "Kayýt isteði boþ.", isSuccess = false });
            }

            var response = _authService.Signup(signupRequest);
            if (!string.IsNullOrEmpty(response.errorMessage))
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            var email = _authService.GetCurrentUserEmail();
            if (email == null)
            {
                return Unauthorized();
            }

            return Ok(new { email });
        }

    }
}
