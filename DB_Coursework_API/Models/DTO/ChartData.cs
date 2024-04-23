namespace DB_Coursework_API.Models.DTO
{
    public class ChartData
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<ChartDataset> Datasets { get; set; } = new List<ChartDataset>();
    }
}
