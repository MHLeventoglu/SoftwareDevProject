using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _cartItemService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _cartItemService.GetById(id);
            if (!result.Success || result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CartItem cartItem)
        {
            var result = _cartItemService.Add(cartItem);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] CartItem cartItem)
        {
            if (id != cartItem.Id)
                return BadRequest("Gönderilen ID ile ürün ID'si uyuşmuyor.");

            var result = _cartItemService.Update(cartItem);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cartItemResult = _cartItemService.GetById(id);
            if (!cartItemResult.Success || cartItemResult.Data == null)
                return NotFound(cartItemResult);

            var result = _cartItemService.Delete(cartItemResult.Data);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}