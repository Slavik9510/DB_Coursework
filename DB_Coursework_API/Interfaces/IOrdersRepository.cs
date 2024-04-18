using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<bool> PlaceOrder(int customerId, string city, string address,
            string postalCode, CartItem[] cartItems);
    }
}
