using DB_Coursework_API.Extensions;
using DB_Coursework_API.Helpers;
using DB_Coursework_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet("items-to-order")]
        public async Task<IActionResult> GetItemsToOrder([FromQuery] PaginationParams paginationParams)
        {

            var items = await _inventoryRepository.GetItemsToOrderAsync(paginationParams);

            Response.AddPaginationHeader(new PaginationHeader(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages));

            return Ok(items);
        }
    }
}
