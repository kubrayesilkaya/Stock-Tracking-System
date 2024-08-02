namespace Capstone.Models.ResponseModel.Order
{
    public class MaterialResponseModel
    {
        public int idMaterial { get; set; }
        public string materialName { get; set; }
        public decimal materialStock { get; set; }
        public string warehouseName {  get; set; }
        public string materialDesc { get; set; }
        public int idWarehouse { get; set; }
        public int idUnit { get; set; }
        public string unitName { get; set; }
    }
}
