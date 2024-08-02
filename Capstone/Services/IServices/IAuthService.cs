using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel;

namespace Capstone.Services.IServices
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email); // Yeni metot eklendi
        AuthResponseModel Login(LoginRequestModel loginRequest);
        AuthResponseModel Signup(SignupRequestModel signupRequest);
        string GetCurrentUserEmail();

    }
}
