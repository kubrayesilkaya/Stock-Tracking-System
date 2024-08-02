using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class WH_MATERIAL
    {
        [Key]
        public int ID_WH_MATERIAL { get; set; }
        public int ID_WAREHOUSE { get; set; }
        public int ID_MATERIAL { get; set; }
        public decimal MATERIAL_STOCK { get; set; }

    }
}
