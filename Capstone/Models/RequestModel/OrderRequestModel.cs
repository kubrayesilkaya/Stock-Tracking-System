namespace Capstone.Models.RequestModel
{
    public class OrderRequestModel
    {
        public int idOrder { get; set; }
        public int idCustomer { get; set; }
        public string location { get; set; }
        public List<OrderDetailRequestModel> OrderDetails { get; set; }
    }

}
