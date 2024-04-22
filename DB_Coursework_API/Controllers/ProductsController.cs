using DB_Coursework_API.Extensions;
using DB_Coursework_API.Helpers;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMyLogger _logger;

        public ProductsController(IProductsRepository productsRepository, IMyLogger logger)
        {
            _productsRepository = productsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams)
        {
            int? userId = User.TryGetUserId();
            if (userId == null)
                await _logger.LogAsync($"Unauthorized user attempting to retrieve products with parameters: {productParams}.");
            else
                await _logger.LogAsync($"User {userId} attempting to retrieve productswith parameters: {productParams}.");

            var products = await _productsRepository.GetProductsAsync(productParams);

            Response.AddPaginationHeader(new PaginationHeader(products.CurrentPage, products.PageSize,
                products.TotalCount, products.TotalPages));

            if (userId == null)
                await _logger.LogAsync("Products retrieved successfully for unauthorized user.");
            else
                await _logger.LogAsync($"Products retrieved successfully for user {userId}.");

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            int? userId = User.TryGetUserId();
            if (userId == null)
                await _logger.LogAsync($"Unauthorized user attempting to retrieve details for product ID: {id}");
            else
                await _logger.LogAsync($"User {userId} attempting to retrieve details for product ID: {id}");

            ProductDetailsDto? product = await _productsRepository.GetDetailsAsync(id);

            if (product == null)
            {
                if (userId == null)
                    await _logger.LogWarningAsync($"Product details not found for ID: {id}. " +
                        $"(Request from unauthorized user)");
                else
                    await _logger.LogWarningAsync($"Product details not found for ID: {id}. " +
                        $"(Request from user {userId})");

                return BadRequest();
            }
            if (userId == null)
                await _logger.LogAsync($"Product details retrieved for ID: {id}. (Request from unauthorized user)");
            else
                await _logger.LogAsync($"Product details retrieved for ID: {id}. (Request from user {userId})");

            return Ok(product);
        }
    }
}
