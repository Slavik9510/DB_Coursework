using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly string _connectionString;
        public EmployeesRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Employees WHERE Email = @Email";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", email);


            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    ID = (int)reader["EmployeeID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    HireDate = (DateTime)reader["HireDate"],
                    Position = (string)reader["Position"],
                    Salary = (decimal)reader["Salary"],
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
