using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.RequestModel
{
    public class LoginRequestModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string factoryType { get; set; } // "PRODUCT" veya "DEALER"
    }
}
