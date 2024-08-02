using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("MATERIAL_SUPPLIER")]
    public class MATERIAL_SUPPLIER
    {
        [Key]
        public int ID_MATERIAL_SUPPLIER { get; set; }
        public int ID_MATERIAL { get; set; }
        public int ID_SUPPLIER { get; set; }
        public string SUPPLIER_CODE { get; set; }
    }
}
