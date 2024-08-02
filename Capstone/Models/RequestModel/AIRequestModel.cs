namespace Capstone.Models.RequestModel
{
    public class AIRequestModel
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int topN { get; set; } = 5; // Default değer atandı
        public string productName { get; set; }
    }
}
