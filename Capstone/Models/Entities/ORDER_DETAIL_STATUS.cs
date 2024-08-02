using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("ORDER_DETAIL_STATUS")]

    public class ORDER_DETAIL_STATUS
    {
        [Key]
        public int ID_ORDER_DETAIL_STATUS { get; set; }
        public string ORDER_DETAIL_STATUS_NAME { get; set; }
    }
}
