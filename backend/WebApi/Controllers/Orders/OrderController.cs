using Business.Abstract.Orders;
using Core.Extensions;
using Entities.Concrete.Orders;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // Requires authentication for all actions
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin)]  // Only admins can see all orders
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("user")]
        public IActionResult GetUserOrders()
        {
            // Get current user's ID from their claims
            var userId = User.GetUserId();
            
            var result = _orderService.GetOrdersByUserId(userId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var result = _orderService.GetById(id);
            if (!result.Success || result.Data == null)
                return NotFound(result);

            // Check if user owns this order or is an admin
            var userId = User.GetUserId();
            if (result.Data.CustomerId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            // Ensure the order is created for the current user
            var userId = User.GetUserId();
            order.CustomerId = userId;

            var result = _orderService.Add(order);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
