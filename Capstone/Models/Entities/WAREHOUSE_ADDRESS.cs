using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class WAREHOUSE_ADDRESS
    {
        [Key]
        public int ID_WAREHOUSE_ADDRESS { get; set; }
        public int ID_WAREHOUSE { get; set; }
        public int ID_COUNTRY { get; set; }
        public int ID_CITY { get; set; }
        public int ID_DISTRICT { get; set; }
        public string WAREHOUSE_FULL_ADDRESS { get; set; }
    }
}
