using DB_Coursework_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "developer")]
    public class LogsController : ControllerBase
    {
        private readonly IMyLogReader _logReader;

        public LogsController(IMyLogReader logReader)
        {
            _logReader = logReader;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] string date)
        {
            DateTime logDate;
            if (string.IsNullOrEmpty(date))
            {
                logDate = DateTime.Today;
            }
            else
            {
                if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out logDate))
                {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd' format.");
                }
            }

            IEnumerable<string> logs = await _logReader.GetLogEntriesAsync(logDate);

            if (logs == null)
            {
                return NotFound($"No logs found for {logDate:yyyy-MM-dd}.");
            }

            return Ok(logs);
        }
    }
}
