using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.User + "," + Roles.Admin)]
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
            // Add user verification
            var userIdClaim = User.FindFirst("nameid")?.Value;
            if (userIdClaim != userId.ToString())
            {
                // return Forbid();
            }

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

        [HttpPost("additem")]
        public IActionResult AddItemToCart([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            var result = _cartService.AddItemToCart(userId, productId, quantity);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("removeitem")]
        public IActionResult RemoveItemFromCart([FromQuery] int userId, [FromQuery] int productId)
        {
            var result = _cartService.RemoveItemFromCart(userId, productId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}