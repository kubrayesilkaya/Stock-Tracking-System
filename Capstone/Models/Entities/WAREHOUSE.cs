using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("WAREHOUSE")]

    public class WAREHOUSE
    {
        [Key]
        public int ID_WAREHOUSE { get; set; }
        public string WAREHOUSE_NAME { get; set; }
        public string CONTACT_PERSON { get; set; }
        public string CONTACT_PERSON_PHONE_CODE { get; set; }
        public string CONTACT_PERSON_PHONE { get; set; }
    }
}
