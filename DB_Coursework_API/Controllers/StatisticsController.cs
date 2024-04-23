using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("sales-by-period")]
        public async Task<IActionResult> GetSalesByPeriod([FromQuery] string startDate, [FromQuery] string endDate)
        {
            DateTime parsedStartDate;
            DateTime parsedEndDate;

            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None,
                out parsedStartDate))
            {
                return BadRequest("Invalid start date format. Use yyyy-MM-dd format.");
            }

            if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None,
                out parsedEndDate))
            {
                return BadRequest("Invalid end date format. Use yyyy-MM-dd format.");
            }

            //ChartData data = await _statisticRepository.GetSalesByPeriodAsync(parsedStartDate, parsedEndDate);
            ChartData data = await _statisticRepository.GetMonthlyOrderRankingsAsync(parsedStartDate, parsedEndDate);

            return Ok(data);
        }
    }
}
