using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("COUNTRY")]
    public class COUNTRY
    {
        [Key]
        public int ID_COUNTRY { get; set; }
        public string COUNTRY_NAME { get; set; }
    }
}
