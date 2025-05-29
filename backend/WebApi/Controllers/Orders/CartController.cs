using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {       
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _cartService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _cartService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            var result = _cartService.GetCartByUserId(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Cart cart)
        {
            var result = _cartService.Add(cart);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Cart cart)
        {
            if (id != cart.Id)
                return BadRequest("ID mismatch");

            var result = _cartService.Update(cart);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartResult = _cartService.GetById(id);
            if (!cartResult.Success)
                return NotFound(cartResult.Message);

            var result = _cartService.Delete(cartResult.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}