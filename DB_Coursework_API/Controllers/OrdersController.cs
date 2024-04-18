using DB_Coursework_API.Extensions;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
        {
            if (await _ordersRepository.PlaceOrder(User.GetUserId(), orderDto.City,
                orderDto.Address, orderDto.PostalCode, orderDto.CartItems))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
