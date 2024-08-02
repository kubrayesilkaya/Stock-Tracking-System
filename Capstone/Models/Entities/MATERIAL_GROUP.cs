using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("MATERIAL_GROUP")]
    public class MATERIAL_GROUP
    {
        [Key]
        public int ID_MATERIAL_GROUP { get; set; }
        public string MATERIAL_GROUP_NAME { get; set; }
        public string MATERIAL_GROUP_DESC { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
