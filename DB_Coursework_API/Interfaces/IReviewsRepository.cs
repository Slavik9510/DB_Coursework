using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IReviewsRepository
    {
        Task<bool> AddReviewAsync(AddReviewDto review, int customerID);
    }
}
