namespace DB_Coursework_API.Models.Domain
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
        //...
    }
}
