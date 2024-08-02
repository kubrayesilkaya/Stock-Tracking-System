using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("DISTRICT")]
    public class DISTRICT
    {
        [Key]
        public int ID_DISTRICT { get; set; }
        public int ID_CITY { get; set; }
        public string DISTRICT_NAME { get; set; }
    }
}
