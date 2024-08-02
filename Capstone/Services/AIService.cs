using Capstone.Models.Data;
using Capstone.Models.RequestModel;
using Capstone.Services.IServices;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Capstone.Services
{
    public class AIService : IAIService
    {
        private readonly string _filePath;
        private readonly MLModelService _mlModelService;

        public AIService()
        {
            _filePath = @"C:\Users\kubra\CapstoneProject\Capstone\Models\Data\DataSet.csv";
            _mlModelService = new MLModelService();
        }

        public List<DataSet> ReadDataSet()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<DataSet>();
                    List<DataSet> dataSetList = records.ToList();
                    return dataSetList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veri okuma hatası: " + ex.Message);
                return new List<DataSet>();
            }
        }

        public List<string> PredictTopProductsForDate(AIRequestModel aiRequest)
        {
            List<DataSet> dataSet = ReadDataSet();

            if (dataSet.Count == 0)
                return new List<string> { "Veri bulunamadı." };

            // Modeli belirli tarih aralığı için eğit
            var model = _mlModelService.TrainModel(dataSet, aiRequest.startDate, aiRequest.endDate);

            // En yoğun tüketilmesi beklenen ürünleri tahmin et
            var topProducts = _mlModelService.PredictTopProducts(model, aiRequest.topN);

            return topProducts;
        }

        public double CalculateAccuracy(AIRequestModel aiRequest)
        {
            List<DataSet> dataSet = ReadDataSet();

            if (dataSet.Count == 0)
                return 0.0;

            // Modeli belirli tarih aralığı için eğit
            var model = _mlModelService.TrainModel(dataSet, aiRequest.startDate, aiRequest.endDate);

            // Doğruluğu hesapla
            return _mlModelService.CalculateAccuracy(dataSet, model, aiRequest.startDate, aiRequest.endDate);
        }

        // Belirli bir ürünün en yoğun tüketildiği ayı tahmin et
        public ProductInfoResponse PredictMostConsumedMonth(AIRequestModel aiRequest)
        {
            List<DataSet> dataSet = ReadDataSet();

            if (dataSet.Count == 0)
                return new ProductInfoResponse { ConsumedMonth = "Veri bulunamadı." };

            // Ürün adına göre filtreleme
            dataSet = dataSet.Where(d => d.materialName.ToLower() == aiRequest.productName.ToLower()).ToList();

            if (dataSet.Count == 0)
                return new ProductInfoResponse { ConsumedMonth = "Ürün bulunamadı." };

            // En eski ve en yeni tarihleri bul
            DateTime minDate = dataSet.Min(d => d.orderDate);
            DateTime maxDate = dataSet.Max(d => d.orderDate);

            // 30 günlük bir aralık oluştur
            DateTime startDate = maxDate.AddDays(-30);
            DateTime endDate = maxDate;

            // Bu tarih aralığı içindeki toplam talebi hesapla
            var totalDemand = dataSet.Where(d => d.orderDate >= startDate && d.orderDate <= endDate)
                                      .Sum(d => d.requestedQuantity);

            return new ProductInfoResponse { ConsumedMonth = $" {startDate:dd-MM-yyyy} - {endDate:dd-MM-yyyy}." };
        }
    }

    public class ProductInfoResponse
    {
        public string ConsumedMonth { get; set; }
    }
}
