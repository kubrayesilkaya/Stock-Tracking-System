namespace Capstone.Models.RequestModel
{
    public class OrderDetailRequestModel
    {
        public int orderDetailId { get; set; }
        public int materialId { get; set; }
        public int requestedQuantity { get; set; }
        public int warehouseId { get; set; }
        public string materialUnit { get; set; }
        public decimal materialPrice { get; set; }

    }
}
