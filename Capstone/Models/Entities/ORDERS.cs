using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class ORDERS
    {
        [Key]
        public int ID_ORDER { get; set; }
        public string LOCATION { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public int ITEM_COUNT { get; set; }
        public decimal ORDER_PRICE { get; set; }
        public int ID_FACTORY_DEALER { get; set; }
        public int ID_ORDER_STATUS { get; set; }
        public int? ID_SERVICE_AREA { get; set; }
        public string DESCRIPTION { get; set; }
        public bool? IS_CANCEL_PROCESS { get; set; }
        public string CANCEL_DESCRIPTION { get; set; }
        public string SELLER_DESCRIPTION { get; set; }
        public int? ID_ORDER_RESPONSIBLE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public bool IS_APPROVED { get; set; }
        public DateTime? APPROVED_DATE { get; set; }
        public int? APPROVED_USER { get; set; }
        public int? ID_USER_RESPONSIBLE { get; set; }
        public List<ORDER_DETAIL> OrderDetails { get; set; } //collection navigation property

        //Bir Order'ın birden fazla orderDetail'i olabilir. Çoğul olduğu için OderDetails.

        //Order'ların birden fazla orderDetail'leri olabileceğinden dolayı, bu Order'a karşılık bir koleksiyon tanımladım,
        //ve bu koleksiyonun ismini de çoğul verdim. Çünkü bu Order'a ait tüm orderDetail'leri öğrenmek istiyor ise, 
        //bunu orderDetails proporty'sinden elde edeceğim.

        //orderDetails tekil olarak Order'a, Order ise çoğul olarak orderDetails'e bağlı. Bire-çok ilişki.
    }
}
