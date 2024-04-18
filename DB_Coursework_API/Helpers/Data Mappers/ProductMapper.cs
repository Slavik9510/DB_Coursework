using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;

namespace DB_Coursework_API.Helpers.Data_Mappers
{
    public class ProductMapper : ISqlDataMapper<ProductDto>
    {
        public ProductDto MapData(SqlDataReader reader)
        {
            var product = new ProductDto()
            {
                ID = (int)reader["ProductID"],
                Name = (string)reader["ProductName"],
                Price = (decimal)reader["UnitPrice"],
                Category = (string)reader["Category"],
                AmountOfComments = (int)reader["NumberOfReviews"],
            };

            if (!reader.IsDBNull(reader.GetOrdinal("Rating")))
            {
                //Rating can be int, so if we read (float)reader => exception
                product.Rating = float.Parse(reader["Rating"].ToString()!);
            }
            else
            {
                product.Rating = 0;
            }

            product.PhotoUrl = !reader.IsDBNull(reader.GetOrdinal("MainImageURL")) ?
                reader["ImageURL"].ToString() : null;

            return product;
        }
    }
}
