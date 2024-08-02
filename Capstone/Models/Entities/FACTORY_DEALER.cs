using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("FACTORY_DEALER")]
    public class FACTORY_DEALER
    {
        [Key]
        public int ID_FACTORY_DEALER { get; set; }
        public string FACTORY_DEALER_EMAIL { get; set; }
        public string FACTORY_DEALER_PASSWORD { get; set; }
        public string FACTORY_DEALER_NAME { get; set; }
        public string FACTORY_DEALER_PHONE_CODE { get; set; }
        public string FACTORY_DEALER_PHONE { get; set; }
        public string CONTACT_PERSON { get; set; }
    }
}
