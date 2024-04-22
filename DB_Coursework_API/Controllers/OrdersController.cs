using DB_Coursework_API.Extensions;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "customer")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMyLogger _logger;

        public OrdersController(IOrdersRepository ordersRepository, IMyLogger logger)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
        {
            await _logger.LogAsync($"Customer {User.GetUserId()} is placing an order.");
            int? orderId = await _ordersRepository.PlaceOrder(User.GetUserId(), orderDto.City,
                orderDto.Address, orderDto.PostalCode, orderDto.Carrier, orderDto.CartItems);
            if (orderId != null)
            {
                await _logger.LogAsync($"Order {orderId} placed successfully for user {User.GetUserId()}.");
                return Ok();
            }

            await _logger.LogWarningAsync($"Failed to place order for user {User.GetUserId()}.");
            return BadRequest();
        }
    }
}
