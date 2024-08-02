using System.ComponentModel.DataAnnotations;

namespace Capstone.Models.Entities
{
    public class ORDER_DETAIL
    {
        [Key]
        public int ID_ORDER_DETAIL { get; set; }
        public int ID_ORDER { get; set; }
        public int ID_WAREHOUSE { get; set; }
        public int REQUESTED_QUANTITY { get; set; }
        public string MATERIAL_UNIT { get; set; }
        public decimal MATERIAL_PRICE { get; set; }
        public int ID_MATERIAL { get; set; }
        public decimal QUANTITY { get; set; }
        public string DESCRIPTION { get; set; }
        public int ID_ORDER_DETAIL_STATUS { get; set; }
        public DateTime? OPERATION_DISPATCHMENT_DATE { get; set; }
        public DateTime? OPERATION_MANUFACTURE_DATE { get; set; }
        public ORDERS Order { get; set; }


        // orderDetail 1 tane Order'a sahip. Order navigation proportysini tekil olarak oluşturuyorum.
        //
        // Oder ise orderDetail'e çoğul olarak bağlı.
        //
        //Her bir orderDetail'in sadece 1 tane Order'ı olacağından dolayı, Order isminde bir navigation proporty tanımladım
        //(ve tekil olacak şekilde tanımladım).
    }
}
