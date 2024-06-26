﻿using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data;
using System.Data.SqlClient;

namespace DB_Coursework_API.Data
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string _connectionString;
        public OrdersRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int?> PlaceOrder(int customerId, string city, string address,
            string postalCode, string carrier, CartItem[] cartItems)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using (var command = new SqlCommand("AddPurchase", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@PostalCode", postalCode);
                command.Parameters.AddWithValue("@Carrier", carrier);

                var orderIdParam = new SqlParameter("@OrderID", SqlDbType.Int);
                orderIdParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(orderIdParam);

                SqlParameter itemsParameter = command.Parameters.AddWithValue("@Items",
                    CreateOrderItemsDataTable(cartItems));
                itemsParameter.SqlDbType = SqlDbType.Structured;
                itemsParameter.TypeName = "dbo.OrderItem";

                await command.ExecuteNonQueryAsync();

                return orderIdParam.Value != DBNull.Value ? (int)orderIdParam.Value : null;
            }
        }

        private DataTable CreateOrderItemsDataTable(CartItem[] items)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ProductID", typeof(int));
            dataTable.Columns.Add("Quantity", typeof(byte));

            foreach (var item in items)
            {
                dataTable.Rows.Add(item.ProductId, item.Quantity);
            }

            return dataTable;
        }
    }
}
