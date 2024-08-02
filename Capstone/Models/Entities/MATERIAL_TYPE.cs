using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("MATERIAL_TYPE")]

    public class MATERIAL_TYPE
    {
        [Key]
        public int ID_MATERIAL_TYPE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string MATERIAL_TYPE_DESC { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
