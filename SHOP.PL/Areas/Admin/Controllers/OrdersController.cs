using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.Models;

namespace SHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus([FromBody] OrderStatus status)
        {
            var orders = await _orderService.GetByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPatch("ChangeStatus/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute]int orderId, [FromBody] OrderStatus newStatus)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, newStatus);
            return Ok(new { Message = "Status is change" });
        }

        [HttpGet("User/{orderId}")]

        public async Task<IActionResult> GetUserByOrder(int orderId)
        {
            var order = await _orderService.GetUserByOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.User);
        }

        [HttpGet("UserOrders/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            var orders = await _orderService.GetOrderByUserAsync(userId);
            return Ok(orders);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var newOrder = await _orderService.AddOrderAsync(order);
            if (newOrder == null)
            {
                return BadRequest("Could not create order");
            }
            return Ok(newOrder);
        }

    }
}
