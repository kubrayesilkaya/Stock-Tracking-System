using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("ORDER_STATUS")]
    public class ORDER_STATUS
    {
        [Key]
        public int ID_ORDER_STATUS { get; set; }
        public string ORDER_STATUS_NAME { get; set; }
    }
}
