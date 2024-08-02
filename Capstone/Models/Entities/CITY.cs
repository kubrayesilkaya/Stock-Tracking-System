using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("CITY")]
    public class CITY
    {
        [Key]
        public int ID_CITY { get; set; }
        public int ID_COUNTRY { get; set; }
        public string CITY_NAME { get; set; }
    }
}
