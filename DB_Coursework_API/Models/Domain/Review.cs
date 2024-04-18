namespace DB_Coursework_API.Models.Domain
{
    public class Review
    {
        public string Reviewer { get; set; }
        public DateTime ReviewDate { get; set; }
        public byte Rating { get; set; }
        public string Content { get; set; }
    }
}
