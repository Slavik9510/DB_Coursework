namespace DB_Coursework_API.Models.DTO
{
    public class OrderDto
    {
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public CartItem[] CartItems { get; set; }
    }
}
