namespace DB_Coursework_API.Helpers
{
    public class ProductParams : PaginationParams
    {
        public string Category { get; set; }
        public string OrderBy { get; set; } = "price";
    }
}
