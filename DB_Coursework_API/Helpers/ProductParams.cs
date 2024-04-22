namespace DB_Coursework_API.Helpers
{
    public class ProductParams : PaginationParams
    {
        public string Category { get; set; }
        public string OrderBy { get; set; } = "price";
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 1000000;

        public override string ToString()
        {
            return $"Category: {Category}, OrderBy: {OrderBy}, MinPrice: {MinPrice}, MaxPrice: {MaxPrice}, " +
                   $"PageNumber: {PageNumber}, PageSize: {PageSize}, OrderDescending: {OrderDescending}";
        }
    }
}
