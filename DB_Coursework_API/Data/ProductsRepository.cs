using DB_Coursework_API.Extensions;
using DB_Coursework_API.Helpers;
using DB_Coursework_API.Helpers.Data_Mappers;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string _connectionString;
        public ProductsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<PagedList<Product>> GetProductsAsync(ProductParams productParams)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            ISqlDataMapper<Product> mapper = new ProductMapper();

            string countQuery = "SELECT COUNT(*) FROM Products WHERE Category = @Category";

            int count = 0;
            using (var command = new SqlCommand(countQuery, connection))
            {
                command.Parameters.AddWithValue("@Category", productParams.Category.FirstCharToUpper());
                count = (int)await command.ExecuteScalarAsync();
            }

            string sortingCriteria = productParams.OrderBy switch
            {
                _ => "UnitPrice"
            };
            string sortingOrder = productParams.OrderDescending ? "DESC" : "";
            string query = "SELECT * FROM Products WHERE Category = @Category " +
                $"ORDER BY {sortingCriteria} {sortingOrder}";

            var paramsValues = new Dictionary<string, object>
            {
                { "@Category", productParams.Category },
            };

            var products = await PagedList<Product>.CreateAsync(connection, query, count, mapper,
                paramsValues, productParams.PageNumber, productParams.PageSize);

            return products;
        }
    }
}
