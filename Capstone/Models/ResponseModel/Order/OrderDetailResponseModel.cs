namespace Capstone.Models.ResponseModel.Order
{
    public class OrderDetailResponseModel
    {
        public int idOrderDetail { get; set; }
        public int idOrder { get; set; }
        public string materialName { get; set; }
        public int idMaterial { get; set; }
        public decimal item_quantity { get; set; }
        public string item_unit { get; set; }
        public string description { get; set; }
        public decimal total_price { get; set; }
        public int idOrderDetailStatus { get; set; }
        public string location { get; set; }
        public string materialCode { get; set; }
        public string unitCode { get; set; }
        public string orderDetailStatusName { get; set; }
    }
}
