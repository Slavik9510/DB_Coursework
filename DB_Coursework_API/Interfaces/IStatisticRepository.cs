using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Interfaces
{
    public interface IStatisticRepository
    {
        Task<ChartData> GetSalesPerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetCityOrdersPerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetAverageOrderPricePerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetTotalSalesByCategoryPerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetAvgOrdersPerCustomerPerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetTopReviewedProductsPerMonthAsync(DateTime startDate, DateTime endDate);
        Task<ChartData> GetTotalReturnedPerMonthAsync(DateTime startDate, DateTime endDate);
    }
}
