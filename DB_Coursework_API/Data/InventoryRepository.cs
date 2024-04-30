using DB_Coursework_API.Helpers;
using DB_Coursework_API.Helpers.Data_Mappers;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string _connectionString;
        public InventoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<PagedList<InventoryItemToOrder>> GetItemsToOrderAsync(PaginationParams paginationParams)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string countQuery = "SELECT COUNT(*) FROM ProductsToOrder";

            int count = 0;

            using (var command = new SqlCommand(countQuery, connection))
            {
                count = (int)await command.ExecuteScalarAsync();
            }

            string query = "SELECT * FROM ProductsToOrder ORDER BY ProductName";

            ISqlDataMapper<InventoryItemToOrder> mapper = new ItemsToOrderMapper();
            var itemsToOrder = await PagedList<InventoryItemToOrder>.CreateAsync(connection, query, count, mapper,
                paginationParams.PageNumber, paginationParams.PageSize);

            return itemsToOrder;
        }
    }
}
