using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticsController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        private async Task<IActionResult> GetChartData(Func<DateTime, DateTime, Task<ChartData>> getDataAsync, string startDate, string endDate)
        {
            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime parsedStartDate))
            {
                return BadRequest("Invalid start date format. Use yyyy-MM-dd format.");
            }

            if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime parsedEndDate))
            {
                return BadRequest("Invalid end date format. Use yyyy-MM-dd format.");
            }

            ChartData data = await getDataAsync(parsedStartDate, parsedEndDate);

            return Ok(data);
        }

        [HttpGet("sales-per-month")]
        public Task<IActionResult> GetSalesPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetSalesPerMonthAsync, startDate, endDate);

        [HttpGet("city-orders-per-month")]
        public Task<IActionResult> GetCityOrdersPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetCityOrdersPerMonthAsync, startDate, endDate);

        [HttpGet("avg-order-price-per-month")]
        public Task<IActionResult> GetAverageOrderPricePerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetAverageOrderPricePerMonthAsync, startDate, endDate);

        [HttpGet("category-sales-per-month")]
        public Task<IActionResult> GetTotalSalesByCategoryPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetTotalSalesByCategoryPerMonthAsync, startDate, endDate);

        [HttpGet("avg-customer-orders-per-month")]
        public Task<IActionResult> GetAvgOrdersPerCustomerPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetAvgOrdersPerCustomerPerMonthAsync, startDate, endDate);

        [HttpGet("products-returned-per-month")]
        public Task<IActionResult> GetProductsReturnedPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetTotalReturnedPerMonthAsync, startDate, endDate);

        [HttpGet("top-reviewed-products-per-month")]
        public Task<IActionResult> GetTopReviewedProductsPerMonth([FromQuery] string startDate, [FromQuery] string endDate) =>
            GetChartData(_statisticRepository.GetTopReviewedProductsPerMonthAsync, startDate, endDate);
    }
}
