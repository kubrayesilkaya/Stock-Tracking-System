using Capstone.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Services
{
    public class MLModelService
    {
        public Dictionary<string, double> TrainModel(List<DataSet> dataSet, DateTime? startDate, DateTime? endDate)
        {
            // Tarih aralığına göre filtreleme
            if (startDate.HasValue && endDate.HasValue)
            {
                dataSet = dataSet.Where(d => d.orderDate.Date >= startDate.Value.Date && d.orderDate.Date <= endDate.Value.Date).ToList();
            }

            // Aylara göre toplam talebi hesapla
            var monthlyDemand = dataSet.GroupBy(d => new { d.materialName, d.orderDate.Month })
                                       .Select(g => new { g.Key.materialName, g.Key.Month, TotalDemand = g.Sum(d => d.requestedQuantity) })
                                       .ToList();

            // Ortalama talebi hesapla
            var averageDemand = monthlyDemand.GroupBy(d => d.materialName)
                                             .Select(g => new { g.Key, AverageDemand = g.Average(d => d.TotalDemand) })
                                             .ToDictionary(d => d.Key, d => d.AverageDemand);

            return averageDemand;
        }

        public List<string> PredictTopProducts(Dictionary<string, double> model, int topN = 5)
        {
            // En yüksek tahmini talebe sahip ürünleri bul
            return model.OrderByDescending(x => x.Value)
                        .Take(topN)
                        .Select(x => x.Key)
                        .ToList();
        }

        public double CalculateAccuracy(List<DataSet> dataSet, Dictionary<string, double> model, DateTime? startDate, DateTime? endDate)
        {
            // Gerçek talep verileri
            var actualDemand = dataSet.Where(d => d.orderDate.Date >= startDate.Value.Date && d.orderDate.Date <= endDate.Value.Date)
                                      .GroupBy(d => d.materialName)
                                      .Select(g => new { g.Key, TotalDemand = g.Sum(d => d.requestedQuantity) })
                                      .ToDictionary(d => d.Key, d => d.TotalDemand);

            // Model tahminleri
            var predictedDemand = model.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Doğruluk hesaplama
            double accuracy = actualDemand.Select(kvp =>
            {
                if (predictedDemand.ContainsKey(kvp.Key))
                {
                    return 1.0 - Math.Abs(kvp.Value - predictedDemand[kvp.Key]) / kvp.Value;
                }
                return 0.0;
            }).Average();

            return accuracy;
        }
    }
}
