using Microsoft.AspNetCore.Mvc;
using Capstone.Services.IServices;
using System;
using System.Collections.Generic;
using Capstone.Models.RequestModel;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet("ReadDataSet")]
        public IActionResult GetDataSet()
        {
            var dataSet = _aiService.ReadDataSet();
            return Ok(dataSet);
        }

        [HttpPost("PredictTopProducts")]
        public IActionResult PredictTopProducts([FromBody] AIRequestModel aiRequest)
        {
            var topProducts = _aiService.PredictTopProductsForDate(aiRequest);
            return Ok(topProducts);
        }

        [HttpPost("CalculateAccuracy")]
        public IActionResult CalculateAccuracy([FromBody] AIRequestModel aiRequest)
        {
            var accuracy = _aiService.CalculateAccuracy(aiRequest);
            return Ok(accuracy);
        }

        [HttpPost("PredictMostConsumedMonth")] // API rotası
        public IActionResult PredictMostConsumedMonth([FromBody] AIRequestModel aiRequest)
        {
            try
            {
                var result = _aiService.PredictMostConsumedMonth(aiRequest);
                // Başarılı yanıt
                return Ok(new { consumedMonth = result.ConsumedMonth });
            }
            catch (Exception ex)
            {
                // Hata durumunda SweetAlert ile hata mesajını göster
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}
