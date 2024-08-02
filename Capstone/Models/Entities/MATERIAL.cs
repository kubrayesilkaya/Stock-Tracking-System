using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("MATERIAL")]
    public class MATERIAL
    {
        [Key]
        public int ID_MATERIAL { get; set; }
        public string MATERIAL_CODE { get; set; }
        public string MATERIAL_NAME { get; set; }
        public string MATERIAL_DESC { get; set; }
        public int ID_UNIT { get; set; }
    }
}
