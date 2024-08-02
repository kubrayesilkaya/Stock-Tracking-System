namespace Capstone.Models.ResponseModel.Order
{
    public class ReportsResponseModel
    {
        public int idOrder { get; set; }
        public int idCustomer { get; set; }
        public int idOrderDetail { get; set; }
        public int idWarehouse { get; set; }
        public string warehouseName { get; set; }
        public string warehouseFullAddress { get; set; }
        public string warehouseCountry { get; set; }
        public string warehouseCity { get; set; }
        public string warehouseDistrict { get; set; }
        public int requestedQuantity { get; set; }
        public int idMaterial { get; set; }
        public string materialName { get; set; }
        public DateTime orderDate { get; set; }
        public decimal materialStock { get; set; }
    }
}
