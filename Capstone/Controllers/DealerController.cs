using Capstone.Models.Core;
using Capstone.Models.RequestModel;
using Capstone.Models.ResponseModel.Order;
using Capstone.Services;
using Capstone.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealerController : Controller
    {
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [HttpGet("GetMaterialComboList")]
        public IActionResult GetMaterialComboList()
        {
            var materialResponseModels = _dealerService.GetMaterialComboList();
            return Ok(materialResponseModels);
        }

        [Authorize]
        [HttpGet("GetOrdersList")]
        public IActionResult GetOrdersList()
        {
            var ordersResponseModel = _dealerService.GetOrdersList();
            return Ok(ordersResponseModel);
        }

        [HttpGet("GetWarehouseInfo")]
        public IActionResult GetWarehouseInfo()
        {
            var reportsResponseModel = _dealerService.GetWarehouseInfo();
            return Ok(reportsResponseModel);
        }

        [HttpPost("InsertOrder")]
        public ActionResult<IEnumerable<string>> InsertOrder([FromBody] OrderRequestModel orderRequest)
        {
            var response = _dealerService.InsertOrder(orderRequest);

            return Ok(response);
        }


    }
}
