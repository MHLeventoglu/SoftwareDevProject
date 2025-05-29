using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    { private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserOrders(int userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpGet("detail/{id}")]// burası düzeltilecek
        public IActionResult GetOrderDetail(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            var result = _orderService.Add(order);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
