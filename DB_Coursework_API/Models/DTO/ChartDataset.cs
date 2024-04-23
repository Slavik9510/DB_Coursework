namespace DB_Coursework_API.Models.DTO
{
    public class ChartDataset
    {
        public string Label { get; set; }
        public List<float?> Data { get; set; } = new List<float?>();
    }
}
