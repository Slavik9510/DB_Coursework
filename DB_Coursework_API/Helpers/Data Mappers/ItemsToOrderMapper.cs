using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;

namespace DB_Coursework_API.Helpers.Data_Mappers
{
    public class ItemsToOrderMapper : ISqlDataMapper<InventoryItemToOrder>
    {
        public InventoryItemToOrder MapData(SqlDataReader reader)
        {
            var item = new InventoryItemToOrder()
            {
                ProductName = (string)reader["ProductName"],
                UnitsInStock = (int)reader["UnitsInStock"],
                ReorderLevel = (int)reader["ReorderLevel"],
                UnitsOnOrder = (int)reader["UnitsOnOrder"],
                SupplierCompanyName = (string)reader["SupplierCompanyName"],
                SupplierEmail = (string)reader["Email"],
                SupplierPhoneNumber = (string)reader["PhoneNumber"]
            };

            return item;
        }
    }
}
