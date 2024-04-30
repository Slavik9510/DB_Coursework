namespace DB_Coursework_API.Models.DTO
{
    public class AddReviewDto
    {
        public int ProductID { get; set; }
        public byte Rating { get; set; }
        public string? Content { get; set; }
    }
}
