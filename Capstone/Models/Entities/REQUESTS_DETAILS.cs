using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class REQUESTS_DETAILS
    {
        [Key]
        public int ID_REQUEST_DETAIL { get; set; }
        public int ID_REQUEST { get; set; }
        public int ID_WAREHOUSE { get; set; }
        public int REQUESTED_QUANTITY { get; set; }
        public string MATERIAL_UNIT { get; set; }
        public decimal MATERIAL_PRICE { get; set; }
        public int ID_MATERIAL { get; set; }
        public decimal QUANTITY { get; set; }
        public string DESCRIPTION { get; set; }
        public REQUESTS Request { get; set; }
    }
}
