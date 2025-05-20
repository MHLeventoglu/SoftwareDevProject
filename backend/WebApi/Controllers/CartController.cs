using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var cart = _cartService.GetCartByUserId(userId);
            return Ok(cart);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CartItem item)
        {
            _cartService.Add(item);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            var result = _cartService.Delete(productId);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
