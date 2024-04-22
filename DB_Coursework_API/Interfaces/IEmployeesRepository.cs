using DB_Coursework_API.Models.Domain;

namespace DB_Coursework_API.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee> GetEmployeeByEmailAsync(string email);
    }
}
