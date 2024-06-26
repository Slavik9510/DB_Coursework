﻿using DB_Coursework_API.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DB_Coursework_API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(SqlConnection connection, string query, int totalItemsCount,
            ISqlDataMapper<T> mapper, int pageNumber, int pageSize, Dictionary<string, object>? paramsValues = null)
        {
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            if (paramsValues == null)
                paramsValues = new Dictionary<string, object>();

            int offset = (pageNumber - 1) * pageSize;
            query += " OFFSET @Offset ROWS FETCH NEXT @Take ROWS ONLY";
            paramsValues.Add("@Offset", offset);
            paramsValues.Add("@Take", pageSize);

            using (var command = new SqlCommand(query, connection))
            {
                foreach (var param in paramsValues)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                var items = new List<T>();
                while (await reader.ReadAsync())
                {
                    T item = mapper.MapData(reader);
                    items.Add(item);
                }
                return new PagedList<T>(items, totalItemsCount, pageNumber, pageSize);
            }
        }
    }
}
