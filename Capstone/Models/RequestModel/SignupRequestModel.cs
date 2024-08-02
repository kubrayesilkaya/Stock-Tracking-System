using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.RequestModel
{
    public class SignupRequestModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string factoryType { get; set; }
        public string factoryName { get; set; }
    }
}
