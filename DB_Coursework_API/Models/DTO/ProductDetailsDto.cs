using DB_Coursework_API.Models.Domain;

namespace DB_Coursework_API.Models.DTO
{
    public class ProductDetailsDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
