using Capstone.Models.DBContext;
using Capstone.Models.ResponseModel.Order;
using Capstone.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models.Core;
using Capstone.Models.Entities;
using Capstone.Models.RequestModel;
using System.Security.Claims;

namespace Capstone.Services
{
    public class DealerService : IDealerService
    {

        private readonly CAPSTONE_CONTEXT _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DealerService(CAPSTONE_CONTEXT dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Material

        public List<MaterialResponseModel> GetMaterialComboList()
        {
            List<MaterialResponseModel> result = new List<MaterialResponseModel>();
            using (var dbContext  = new CAPSTONE_CONTEXT())
            {
                try
                {
                    result = (from m in dbContext.Set<MATERIAL>()
                              join whm in dbContext.Set<WH_MATERIAL>() on m.ID_MATERIAL equals whm.ID_MATERIAL
                              join w in dbContext.Set<WAREHOUSE>() on whm.ID_WAREHOUSE equals w.ID_WAREHOUSE
                              select new MaterialResponseModel
                              {
                                  idMaterial = m.ID_MATERIAL,
                                  idWarehouse = w.ID_WAREHOUSE,
                                  materialName = m.MATERIAL_NAME,
                                  materialStock = whm.MATERIAL_STOCK,
                                  warehouseName = w.WAREHOUSE_NAME
                              }).ToList();
                }
                catch (Exception ex)
                {

                }
                return result;
            }
        }

        #endregion

        #region reportOrders

        public List<OrdersResponseModel> GetOrdersList()
        {
            List<OrdersResponseModel> result = new List<OrdersResponseModel>();

            // Aktif kullanıcının email adresini al
            var dealerEmail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(dealerEmail))
            {
                // Eğer email adresi alınamazsa, boş listeyi veya uygun bir hata mesajını döndürebilirsiniz.
                return new List<OrdersResponseModel>();
            }

            using (var dbContext = new CAPSTONE_CONTEXT())
            {
                try
                {
                    // FACTORY_DEALER tablosundan ilgili kullanıcının ID'sini al
                    var dealerId = dbContext.FACTORY_DEALER.FirstOrDefault(d => d.FACTORY_DEALER_EMAIL == dealerEmail)?.ID_FACTORY_DEALER;

                    if (dealerId == null)
                    {
                        // Eğer kullanıcı bulunamazsa, uygun bir hata mesajı veya işlemi döndürebilirsiniz.
                        return new List<OrdersResponseModel>();
                    }

                    // Siparişleri çek ve ilgili kullanıcının ID'sine göre filtrele
                    result = (from o in dbContext.Set<ORDERS>()
                              join od in dbContext.Set<ORDER_DETAIL>() on o.ID_ORDER equals od.ID_ORDER
                              join m in dbContext.Set<MATERIAL>() on od.ID_MATERIAL equals m.ID_MATERIAL
                              join whm in dbContext.Set<WH_MATERIAL>() on m.ID_MATERIAL equals whm.ID_MATERIAL
                              join w in dbContext.Set<WAREHOUSE>() on whm.ID_WAREHOUSE equals w.ID_WAREHOUSE
                              where o.ID_FACTORY_DEALER == dealerId
                              select new OrdersResponseModel
                              {
                                  idOrder = o.ID_ORDER,
                                  idCustomer = o.ID_FACTORY_DEALER,
                                  idOrderDetail = od.ID_ORDER_DETAIL,
                                  idWarehouse = whm.ID_WAREHOUSE,
                                  warehouseName = w.WAREHOUSE_NAME,
                                  requestedQuantity = od.REQUESTED_QUANTITY,
                                  materialName = m.MATERIAL_NAME,
                                  orderDate = o.ORDER_DATE
                              }).ToList();
                }
                catch (Exception ex)
                {
                    // Hata durumunda uygun bir işlem yapabilirsiniz.
                }
                return result;
            }
        }

        #endregion

        #region

        public List<ReportsResponseModel> GetWarehouseInfo()
        {
            List<ReportsResponseModel> result = new List<ReportsResponseModel>();
            using (var dbContext = new CAPSTONE_CONTEXT())
            {
                try
                {
                    result = (from o in dbContext.Set<ORDERS>()
                              join od in dbContext.Set<ORDER_DETAIL>() on o.ID_ORDER equals od.ID_ORDER
                              join m in dbContext.Set<MATERIAL>() on od.ID_MATERIAL equals m.ID_MATERIAL
                              join whm in dbContext.Set<WH_MATERIAL>() on m.ID_MATERIAL equals whm.ID_MATERIAL
                              join w in dbContext.Set<WAREHOUSE>() on whm.ID_WAREHOUSE equals w.ID_WAREHOUSE
                              join wa in dbContext.Set<WAREHOUSE_ADDRESS>() on w.ID_WAREHOUSE equals wa.ID_WAREHOUSE
                              join d in dbContext.Set<DISTRICT>() on wa.ID_DISTRICT equals d.ID_DISTRICT
                              join ci in dbContext.Set<CITY>() on d.ID_CITY equals ci.ID_CITY
                              join co in dbContext.Set<COUNTRY>() on ci.ID_COUNTRY equals co.ID_COUNTRY
                              
                              select new ReportsResponseModel
                              {
                                  idWarehouse = whm.ID_WAREHOUSE,
                                  warehouseName = w.WAREHOUSE_NAME,
                                  warehouseFullAddress = wa.WAREHOUSE_FULL_ADDRESS,
                                  warehouseDistrict = d.DISTRICT_NAME,
                                  warehouseCity = ci.CITY_NAME,
                                  warehouseCountry = co.COUNTRY_NAME,
                                  materialName = m.MATERIAL_NAME,
                                  materialStock = whm.MATERIAL_STOCK
                              }).ToList();
                }
                catch (Exception ex)
                {

                }
                return result;
            }
        }

        #endregion

        #region addOrderV2

        public string InsertOrder(OrderRequestModel orderRequest)
        {
            using (var dbContext = new CAPSTONE_CONTEXT())
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var newRecord = new ORDERS
                    {
                        //ID_CUSTOMER = orderRequest.idCustomer,
                        ORDER_DATE = DateTime.Now,
                    };
                    dbContext.Set<ORDERS>().Add(newRecord);
                    dbContext.SaveChanges();

                    // Order Detail 

                    List<ORDER_DETAIL> detailList = new List<ORDER_DETAIL>();
                    foreach (var item in orderRequest.OrderDetails)
                    {
                        var newRecordOrderDetail = new ORDER_DETAIL
                        {
                            ID_ORDER = newRecord.ID_ORDER,
                            ID_MATERIAL = item.materialId,
                            ID_WAREHOUSE = item.warehouseId,
                            REQUESTED_QUANTITY = item.requestedQuantity,
                            //MATERIAL_PRICE = item.materialPrice
                        };

                        detailList.Add(newRecordOrderDetail);
                    }

                    dbContext.Set<ORDER_DETAIL>().AddRange(detailList);
                    dbContext.SaveChanges();

                    // Stock Update
                    string emptyStockMessage = "";

                    foreach (var orderItem in orderRequest.OrderDetails)
                    {
                        var existMaterial = _dbContext.Set<WH_MATERIAL>().Where(x => x.ID_MATERIAL == orderItem.materialId && x.ID_WAREHOUSE == orderItem.warehouseId).FirstOrDefault();

                        if (existMaterial != null)
                        {
                            if (orderItem.requestedQuantity <= existMaterial.MATERIAL_STOCK)
                            {
                                existMaterial.MATERIAL_STOCK -= orderItem.requestedQuantity;
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                emptyStockMessage += existMaterial.ID_MATERIAL + " idli malzeme için stok yetersiz. ";

                                // Stok yetersizse REQUESTS tablosuna kayıt ekle
                                var newRequest = new REQUESTS
                                {
                                    LOCATION = "Yetersiz Stok Lokasyonu",
                                    REQUEST_DATE = DateTime.Now,
                                    ITEM_COUNT = orderItem.requestedQuantity,
                                    REQUEST_PRICE = 0, // Fiyat bilgisi eklenebilir
                                    ID_FACTORY_DEALER = 0, // Fabrika/Bayi bilgisi eklenebilir
                                    DESCRIPTION = emptyStockMessage,
                                    ID_USER_RESPONSIBLE = null, // Sorumlu kullanıcı bilgisi eklenebilir
                                    RequestDetails = new List<REQUESTS_DETAILS>
                            {
                                new REQUESTS_DETAILS
                                {
                                    ID_WAREHOUSE = orderItem.warehouseId,
                                    REQUESTED_QUANTITY = orderItem.requestedQuantity,
                                    MATERIAL_UNIT = "Adet", // Birim bilgisi eklenebilir
                                    MATERIAL_PRICE = 0, // Fiyat bilgisi eklenebilir
                                    ID_MATERIAL = orderItem.materialId,
                                    QUANTITY = orderItem.requestedQuantity,
                                    DESCRIPTION = "Yetersiz stok nedeniyle istek oluşturuldu"
                                }
                            }
                                };
                                dbContext.Set<REQUESTS>().Add(newRequest);
                                dbContext.SaveChanges();

                                //throw new Exception(emptyStockMessage);
                            }
                        }
                    }

                
                    dbContextTransaction.Commit();
                    return "Sipariş kaydedildi.";

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return "Sipariş kaydedilirken hata oluştu; sipariş kaydedilemedi: " + ex.Message;
                }
            }
        }

        #endregion
    }
}
