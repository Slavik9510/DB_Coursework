using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IStatisticRepository
    {
        Task<ChartData> GetSalesByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetMonthlyOrderRankingsAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetAverageOrderPriceByMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetTotalSalesByCategoryAndMonthAsync(int year);
        Task<ChartData> GetAvgOrdersPerCustomerByMonthAsync(DateTime startDate, DateTime endDate);
    }
}
