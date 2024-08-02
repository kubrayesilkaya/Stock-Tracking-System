using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class REQUESTS
    {
        [Key]
        public int ID_REQUEST { get; set; }
        public string LOCATION { get; set; }
        public DateTime REQUEST_DATE { get; set; }
        public int ITEM_COUNT { get; set; }
        public decimal REQUEST_PRICE { get; set; }
        public int ID_FACTORY_DEALER { get; set; }
        public string DESCRIPTION { get; set; }
        public int? ID_USER_RESPONSIBLE { get; set; }
        public List<REQUESTS_DETAILS> RequestDetails { get; set; }
    }
}
