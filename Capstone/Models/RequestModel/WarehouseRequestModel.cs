namespace Capstone.Models.RequestModel
{
    public class WarehouseRequestModel
    {
        public string warehouseName { get; set; }
        public string contactPerson { get; set; }
        public string contactPersonPhoneCode { get; set; }
        public string contactPersonPhone { get; set; }
        public int warehouseCountry { get; set; }
        public int warehouseCity { get; set; }
        public int warehouseDistrict { get; set; }
        public string warehouseFullAddress { get; set; }

    }
}
