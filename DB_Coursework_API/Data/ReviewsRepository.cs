using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly string _connectionString;
        public ReviewsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<bool> AddReviewAsync(AddReviewDto review, int customerID)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "INSERT INTO Reviews VALUES " +
                "(@CustomerID, @ProductID, @ReviewDate, @Rating, @ReviewText)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerID", customerID);
            command.Parameters.AddWithValue("@ProductID", review.ProductID);
            command.Parameters.AddWithValue("@ReviewDate", DateTime.Now);
            command.Parameters.AddWithValue("@Rating", review.Rating);
            command.Parameters.AddWithValue("@ReviewText", (review.Content != null) ? review.Content : DBNull.Value);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
