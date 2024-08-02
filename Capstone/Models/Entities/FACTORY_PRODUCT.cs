using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("FACTORY_PRODUCT")]
    public class FACTORY_PRODUCT
    {
        [Key]
        public int ID_FACTORY_PRODUCT { get; set; }
        public string FACTORY_PRODUCT_EMAIL { get; set; }
        public string FACTORY_PRODUCT_PASSWORD { get; set; }
        public string FACTORY_PRODUCT_NAME { get; set; }
        public string FACTORY_PRODUCT_PHONE_CODE { get; set; }
        public string FACTORY_PRODUCT_PHONE { get; set; }
        public string CONTACT_PERSON { get; set; }
    }
}
