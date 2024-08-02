using Capstone.Models.Core;
using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel.Order;

namespace Capstone.Services.IServices
{
    public interface IDealerService
    {
        List<MaterialResponseModel> GetMaterialComboList();

        List<OrdersResponseModel> GetOrdersList();
        List<ReportsResponseModel> GetWarehouseInfo();

        string InsertOrder(OrderRequestModel order);


    }
}
