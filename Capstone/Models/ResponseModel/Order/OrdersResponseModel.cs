namespace Capstone.Models.ResponseModel.Order
{
    public class OrdersResponseModel
    {
        public int idOrder { get; set; }
        public int idCustomer { get; set; }
        public int idOrderDetail { get; set; }
        public int idWarehouse { get; set; }
        public string warehouseName { get; set; }
        public int requestedQuantity { get; set; }
        public int idMaterial { get; set; }
        public string materialName { get; set; }
        public DateTime orderDate { get; set; }
    }
}
