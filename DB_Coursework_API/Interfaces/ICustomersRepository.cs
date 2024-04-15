using DB_Coursework_API.Models.Domain;

namespace DB_Coursework_API.Interfaces
{
    public interface ICustomersRepository
    {
        Task<bool> CustomerExistsAsync(string email, string phoneNumber);

        Task<bool> AddCustomerAsync(Customer customer);

        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}
