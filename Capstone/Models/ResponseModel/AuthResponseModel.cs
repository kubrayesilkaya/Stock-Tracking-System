namespace Capstone.Models.ResponseModel
{
    public class AuthResponseModel
    {
        public string errorMessage { get; set; }
        public string successMessage { get; set; }
        public bool isSuccess { get; set; }
        public string Token { get; set; } // JWT tokeni için yeni özellik

    }
}
