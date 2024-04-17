using AutoMapper;
using DB_Coursework_API.Helpers;
using DB_Coursework_API.Helpers.Data_Mappers;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public ProductsRepository(IConfiguration config, IMapper mapper)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
            _mapper = mapper;
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
                command.Parameters.AddWithValue("@Category", productParams.Category);
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


        public async Task<List<ProductDto>> GetAdditionalData(IEnumerable<Product> products)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string commentsQuery = "SELECT Count(*) FROM Products p JOIN Reviews r ON " +
                "r.ProductID = p.ProductID WHERE r.ProductID = @Id";
            string ratingQuery = "SELECT AVG(CAST(Rating AS float)) AS Rating FROM " +
                "Products p JOIN Reviews r ON r.ProductID = p.ProductID WHERE r.ProductID = @Id";
            string photoQuery = "SELECT ImageURL FROM Products p JOIN ProductImages img ON " +
                "img.ProductID = p.ProductID WHERE img.IsMain = @Id";

            List<ProductDto> productDtos = _mapper.Map<List<ProductDto>>(products);
            int count = 0;
            foreach (var product in productDtos)
            {
                using (var command = new SqlCommand(commentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.ID);
                    product.AmountOfComments = (int)await command.ExecuteScalarAsync();
                }
                using (var command = new SqlCommand(ratingQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.ID);
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Rating")))
                        {
                            //Rating can be int, so if we read (float)reader => exception
                            product.Rating = float.Parse(reader["Rating"].ToString());
                        }
                        else
                        {
                            product.Rating = 0;
                        }
                    }
                    await reader.CloseAsync();
                }
                using (var command = new SqlCommand(photoQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.ID);
                    using SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        product.PhotoUrl = !reader.IsDBNull(reader.GetOrdinal("ImageURL")) ?
                            reader["ImageURL"].ToString() : null;
                    }
                    await reader.CloseAsync();
                }
            }

            return productDtos;
        }
    }
}
