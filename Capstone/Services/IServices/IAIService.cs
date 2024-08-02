using Capstone.Models.Data;
using Capstone.Models.RequestModel;
using System;
using System.Collections.Generic;

namespace Capstone.Services.IServices
{
    public interface IAIService
    {
        List<DataSet> ReadDataSet();
        List<string> PredictTopProductsForDate(AIRequestModel aiRequest);
        double CalculateAccuracy(AIRequestModel aiRequest);
        ProductInfoResponse PredictMostConsumedMonth(AIRequestModel aiRequest);

    }
}
