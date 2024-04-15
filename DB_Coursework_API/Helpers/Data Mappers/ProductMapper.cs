using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace DB_Coursework_API.Helpers.Data_Mappers
{
    public class ProductMapper : ISqlDataMapper<Product>
    {
        public Product MapData(SqlDataReader reader)
        {
            string attributes = (string)reader["Attributes"];
            var product = new Product()
            {
                ID = (int)reader["ProductID"],
                Name = (string)reader["ProductName"],
                Price = (decimal)reader["UnitPrice"],
                Category = (string)reader["Category"],
                Description = (string)reader["Description"],
                Attributes = JsonConvert.DeserializeObject<Dictionary<string, object>>(attributes)
            };

            return product;
        }
    }
}
