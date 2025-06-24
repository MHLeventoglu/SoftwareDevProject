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

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _cartService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _cartService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet("getbyuserid/{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            var result = _cartService.GetCartByUserId(userId);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Cart cart)
        {
            var result = _cartService.Add(cart);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Cart cart)
        {
            if (id != cart.Id)
                return BadRequest("Gönderilen ID ile sepet ID'si uyuşmuyor.");

            var result = _cartService.Update(cart);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cartResult = _cartService.GetById(id);
            if (!cartResult.Success || cartResult.Data == null)
                return NotFound(cartResult);

            var result = _cartService.Delete(cartResult.Data);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}