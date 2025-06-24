using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserOrders(int userId)
        {
            var result = _orderService.GetOrdersByUserId(userId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("detail/{id}")]//burası düzeltilecek
        public IActionResult GetOrderDetail(int id)
        {
            var result = _orderService.GetById(id);
            if (!result.Success || result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            var result = _orderService.Add(order);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
