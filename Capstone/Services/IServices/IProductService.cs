using Capstone.Models.Entities;
using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel.Order;

namespace Capstone.Services.IServices
{
    public interface IProductService
    {
        string IncreaseStock(OrderRequestModel orderRequest);

        string AddWarehouse(WarehouseRequestModel warehouseRequest);

    }
}
