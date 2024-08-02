using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Entities
{
    [Table("ORDER_ADDRESS")]

    public class ORDER_ADDRESS
    {
        [Key]
        public int ID_ORDER_ADDRESS { get; set; }
        public int ID_ORDER { get; set; }
        public int ID_CUSTOMER_ADDRESS { get; set; }
        public string CUSTOMER_ADDRESS_NAME { get; set; }
        public string CUSTOMER_ADDRESS_COUNTRY { get; set; }
        public string CUSTOMER_ADDRESS_CITY { get; set; }
        public string CUSTOMER_ADDRESS_DISTRICT { get; set; }
        public string CUSTOMER_COMPANY_NAME { get; set; }
        public string CUSTOMER_ADDRESS_ADDRESS { get; set; }
        public bool IS_COMPANY { get; set; }
        public string CUSTOMER_TAX_NUMBER { get; set; }
        public string CUSTOMER_TAX_OFFICE { get; set; }
    }
}
