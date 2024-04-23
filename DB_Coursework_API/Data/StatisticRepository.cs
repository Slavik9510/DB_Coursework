using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using System.Data.SqlClient;
using System.Globalization;

namespace DB_Coursework_API.Data
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly string _connectionString;
        public StatisticRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<ChartData> GetSalesPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var chartData = new ChartData();
            using (var command = new SqlCommand(
                "SELECT * FROM GetSalesByPeriod(@StartDate, @EndDate) ORDER BY SalePeriod", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                var chartDataset = new ChartDataset();
                chartDataset.Label = "Total products sold";
                while (await reader.ReadAsync())
                {
                    string period = reader["SalePeriod"].ToString();
                    int totalProductsSold = (int)reader["TotalProductsSold"];
                    chartDataset.Data.Add(totalProductsSold);
                    chartData.Labels.Add(ConvertToMonthYearFormat(period));
                }
                chartData.Datasets.Add(chartDataset);
            }

            return chartData;
        }

        public async Task<ChartData> GetCityOrdersPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using (var command = new SqlCommand(
            "SELECT * FROM GetMonthlyOrderRankings(@StartDate, @EndDate) ORDER BY OrderMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                var resultData = new Dictionary<string, Dictionary<string, float>>();
                while (await reader.ReadAsync())
                {
                    string orderMonth = reader["OrderMonth"].ToString();
                    string city = reader["City"].ToString();
                    float totalOrders = Convert.ToSingle(reader["TotalOrders"]);

                    if (!resultData.ContainsKey(orderMonth))
                    {
                        resultData[orderMonth] = new Dictionary<string, float>();
                    }
                    resultData[orderMonth].Add(city, totalOrders);
                }

                return ConvertToChartData(resultData);
            }
        }

        public async Task<ChartData> GetAverageOrderPricePerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var chartData = new ChartData();
            using (var command = new SqlCommand(
                "SELECT * FROM GetAverageOrderPriceByMonth(@StartDate, @EndDate) ORDER BY OrderMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                var chartDataset = new ChartDataset();
                chartDataset.Label = "Average order price ₴";
                while (await reader.ReadAsync())
                {
                    string month = reader["OrderMonth"].ToString();
                    int avgOrderPrice = (int)reader["AvgOrderPrice"];
                    chartDataset.Data.Add(avgOrderPrice);
                    chartData.Labels.Add(ConvertToMonthYearFormat(month));
                }
                chartData.Datasets.Add(chartDataset);
            }

            return chartData;
        }

        public async Task<ChartData> GetTotalSalesByCategoryPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using (var command = new SqlCommand(
            "SELECT * FROM GetTotalSalesByCategoryAndMonth(@StartDate, @EndDate) ORDER BY OrderMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                var resultData = new Dictionary<string, Dictionary<string, float>>();
                while (await reader.ReadAsync())
                {
                    string orderMonth = reader["OrderMonth"].ToString();
                    string category = reader["Category"].ToString();
                    float totalSales = Convert.ToSingle(reader["TotalSales"]);

                    if (!resultData.ContainsKey(orderMonth))
                    {
                        resultData[orderMonth] = new Dictionary<string, float>();
                    }
                    resultData[orderMonth].Add(category, totalSales);
                }

                return ConvertToChartData(resultData);
            }
        }

        public async Task<ChartData> GetAvgOrdersPerCustomerPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var chartData = new ChartData();
            using (var command = new SqlCommand(
                "SELECT * FROM GetAvgOrdersPerCustomerByMonth(@StartDate, @EndDate) ORDER BY OrderMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                var chartDataset = new ChartDataset();
                chartDataset.Label = "Average orders per customer";
                while (await reader.ReadAsync())
                {
                    string month = reader["OrderMonth"].ToString();
                    float avgOrdersPerCustomer = Convert.ToSingle(reader["AvgOrdersPerCustomer"]);
                    chartDataset.Data.Add(avgOrdersPerCustomer);
                    chartData.Labels.Add(ConvertToMonthYearFormat(month));
                }
                chartData.Datasets.Add(chartDataset);
            }

            return chartData;
        }

        public async Task<ChartData> GetTotalReturnedPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var chartData = new ChartData();
            using (var command = new SqlCommand(
                "SELECT * FROM GetTotalReturnedByMonth(@StartDate, @EndDate) ORDER BY ReturnMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                var chartDataset = new ChartDataset();
                chartDataset.Label = "Total product returns";
                while (await reader.ReadAsync())
                {
                    string month = reader["returnMonth"].ToString();
                    int totalReturned = (int)reader["TotalReturned"];
                    chartDataset.Data.Add(totalReturned);
                    chartData.Labels.Add(ConvertToMonthYearFormat(month));
                }
                chartData.Datasets.Add(chartDataset);
            }

            return chartData;
        }

        public async Task<ChartData> GetTopReviewedProductsPerMonthAsync(DateTime startDate, DateTime endDate)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using (var command = new SqlCommand(
            "SELECT * FROM GetTopReviewsByMonth(@StartDate, @EndDate) ORDER BY ReviewMonth", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using SqlDataReader reader = await command.ExecuteReaderAsync();

                var resultData = new Dictionary<string, Dictionary<string, float>>();
                while (await reader.ReadAsync())
                {
                    string orderMonth = reader["ReviewMonth"].ToString();
                    string productName = reader["ProductName"].ToString();
                    float totalReviews = Convert.ToSingle(reader["TotalReviews"]);

                    if (!resultData.ContainsKey(orderMonth))
                    {
                        resultData[orderMonth] = new Dictionary<string, float>();
                    }
                    resultData[orderMonth].Add(productName, totalReviews);
                }

                return ConvertToChartData(resultData);
            }
        }
        private string ConvertToMonthYearFormat(string input)
        {
            string[] parts = input.Split('-');

            if (parts.Length != 2)
            {
                return string.Empty;
            }

            int year;
            if (!int.TryParse(parts[0], out year))
            {
                return string.Empty;
            }

            int month;
            if (!int.TryParse(parts[1], out month) || month < 1 || month > 12)
            {
                return string.Empty;
            }

            CultureInfo ci = new CultureInfo("en-US");
            string monthName = ci.DateTimeFormat.GetMonthName(month);

            // Повертаємо рядок з форматованою назвою місяця та роком
            return $"{char.ToUpper(monthName[0])}{monthName.Substring(1)} {year}";
        }
        private ChartData ConvertToChartData(Dictionary<string, Dictionary<string, float>> data)
        {
            var chartData = new ChartData();
            foreach (var month in data.Keys)
            {
                chartData.Labels.Add(ConvertToMonthYearFormat(month));
                foreach (var city in data[month].Keys)
                {
                    var dataset = chartData.Datasets.Find(d => d.Label == city);
                    if (dataset == null)
                    {
                        dataset = new ChartDataset
                        {
                            Label = city,
                            Data = new List<float?>(new float?[data.Keys.Count])
                        };
                        chartData.Datasets.Add(dataset);
                    }
                    var i = chartData.Labels.IndexOf(ConvertToMonthYearFormat(month));
                    dataset.Data[i] = data[month][city];
                }
            }
            return chartData;
        }
    }
}
