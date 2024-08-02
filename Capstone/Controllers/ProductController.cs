using Capstone.Models.Core;
using Capstone.Models.Entities;
using Capstone.Models.RequestModel;
using Capstone.Services;
using Capstone.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost("IncreaseStock")]
        public ActionResult<IEnumerable<string>> IncreaseStock([FromBody] OrderRequestModel orderRequest)
        {

            var response = _productService.IncreaseStock(orderRequest);

            return Ok(response);
        }

        [HttpPost("add")]
        public IActionResult AddWarehouse([FromBody] WarehouseRequestModel warehouseRequest)
        {
            var result = _productService.AddWarehouse(warehouseRequest);
            return Ok(result);
        }
    }
}
