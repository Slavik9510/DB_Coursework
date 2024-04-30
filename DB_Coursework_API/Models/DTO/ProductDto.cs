namespace DB_Coursework_API.Models.DTO
{
    public class InventoryItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string Category { get; set; }
        public float Rating { get; set; }
        public int AmountOfComments { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
