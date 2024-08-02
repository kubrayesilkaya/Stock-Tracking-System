using Capstone.Models.DBContext;
using Capstone.Models.Entities;
using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel.Order;
using Capstone.Services.IServices;
using System.Data.Entity;

namespace Capstone.Services
{
    public class ProductService : IProductService
    {
        private readonly CAPSTONE_CONTEXT _dbContext;

        public ProductService(CAPSTONE_CONTEXT dbContext)
        {
            _dbContext = dbContext;
        }

        #region Get

        public string IncreaseStock(OrderRequestModel orderRequest)
        {
            using (var dbContext = new CAPSTONE_CONTEXT())
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {

                    // Stock Update
                    string emptyStockMessage = "";

                    foreach (var orderItem in orderRequest.OrderDetails)
                    {
                        var existMaterial = _dbContext.Set<WH_MATERIAL>().Where(x => x.ID_MATERIAL == orderItem.materialId && x.ID_WAREHOUSE == orderItem.warehouseId).FirstOrDefault();

                        if (existMaterial != null)
                        {
                                existMaterial.MATERIAL_STOCK += orderItem.requestedQuantity;
                                _dbContext.SaveChanges();

                        }
                    }

                    dbContext.SaveChanges();

                    dbContextTransaction.Commit();

                    return "Successful";
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Dispose();
                    return "Unsuccessful!";
                }
            }
        }

        #endregion

        #region addWarehouse

        public string AddWarehouse(WarehouseRequestModel warehouseRequest)
        {
            try
            {
                var warehouse = new WAREHOUSE
                {
                    WAREHOUSE_NAME = warehouseRequest.warehouseName,
                    CONTACT_PERSON = warehouseRequest.contactPerson,
                    CONTACT_PERSON_PHONE_CODE = warehouseRequest.contactPersonPhoneCode,
                    CONTACT_PERSON_PHONE = warehouseRequest.contactPersonPhone
                };

                var warehouseAddress = new WAREHOUSE_ADDRESS
                {
                    ID_COUNTRY = warehouseRequest.warehouseCountry,
                    ID_CITY = warehouseRequest.warehouseCity,
                    ID_DISTRICT = warehouseRequest.warehouseDistrict,
                    WAREHOUSE_FULL_ADDRESS = warehouseRequest.warehouseFullAddress
                };

                _dbContext.WAREHOUSE.Add(warehouse);
                _dbContext.WAREHOUSE_ADDRESS.Add(warehouseAddress);
                _dbContext.SaveChanges();

                return "Warehouse added successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to add warehouse!";
            }
        }

        #endregion

    }
}
