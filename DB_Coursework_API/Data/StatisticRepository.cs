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
        public async Task<ChartData> GetSalesByPeriodAsync(DateTime startDate, DateTime endDate)
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

        public async Task<ChartData> GetMonthlyOrderRankingsAsync(DateTime startDate, DateTime endDate)
        {
            var chartData = new ChartData();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                "SELECT * FROM GetMonthlyOrderRankings(@StartDate, @EndDate) ORDER BY OrderMonth, TotalOrders DESC", connection))
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

                    // Preparing data for chart
                    int i = 0;
                    foreach (var month in resultData.Keys)
                    {
                        chartData.Labels.Add(ConvertToMonthYearFormat(month));
                        foreach (var city in resultData[month].Keys)
                        {
                            var dataset = chartData.Datasets.Find(d => d.Label == city);
                            if (dataset == null)
                            {
                                dataset = new ChartDataset
                                {
                                    Label = city,
                                    Data = new List<float?>(new float?[resultData.Keys.Count])
                                };
                                chartData.Datasets.Add(dataset);
                            }
                            dataset.Data[i] = resultData[month][city];
                            //dataset.Data.Add(resultData[month][city]);
                        }
                        i++;
                    }
                }
            }

            return chartData;
        }

        public Task<ChartData> GetAvgOrdersPerCustomerByMonthAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ChartData> GetAverageOrderPriceByMonthAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ChartData> GetTotalSalesByCategoryAndMonthAsync(int year)
        {
            throw new NotImplementedException();
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
    }
}
