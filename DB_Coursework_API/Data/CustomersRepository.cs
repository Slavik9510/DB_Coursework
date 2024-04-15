using DB_Coursework_API.Extensions;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly string _connectionString;
        public CustomersRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "INSERT INTO Customers (FirstName, LastName, PasswordHash, PasswordSalt, Email, " +
                "PhoneNumber) VALUES (@FirstName, @LastName, @PasswordHash, @PasswordSalt, @Email, @PhoneNumber)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", customer.FirstName);
            command.Parameters.AddWithValue("@LastName", customer.LastName);
            command.Parameters.AddWithValue("@PasswordHash", customer.PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", customer.PasswordSalt);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> CustomerExistsAsync(string email, string phoneNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Check if db contains a user with a specific email or phone number
            string query = "IF EXISTS (SELECT 1 FROM Customers WHERE Email = @Email OR " +
                "PhoneNumber = @PhoneNumber) SELECT 1 ELSE SELECT 0";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            return (int)await command.ExecuteScalarAsync() == 1;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Customers WHERE Email = @Email";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            var t = command.GetDebugQueryText();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                return new Customer
                {
                    ID = (int)reader["CustomerID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    PasswordHash = (byte[])reader["PasswordHash"],
                    PasswordSalt = (byte[])reader["PasswordSalt"],
                    Email = (string)reader["Email"],
                    PhoneNumber = (string)reader["PhoneNumber"]
                };
            }
            else return null;
        }
    }
}
