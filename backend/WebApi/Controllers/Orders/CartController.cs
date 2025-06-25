using System.Security.Claims;
using Business.Abstract.Orders;
using Entities.Concrete.Orders;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetAll()
        {
            var result = _cartService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _cartService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet("getbyuser")]
        public IActionResult GetCartByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) {
                return BadRequest("You need to be logged in.");
            }

            var result = _cartService.GetCartByUserId(userId);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("add")]
        public IActionResult Add([FromBody] Cart cart)
        {
            var result = _cartService.Add(cart);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
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
        
        [Authorize(Roles = Roles.Admin)]
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
        public IActionResult AddItemToCart([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) {
                return BadRequest("You need to be logged in.");
            }
            var result = _cartService.AddItemToCart(userId, productId, quantity);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("removeitem")]
        public IActionResult RemoveItemFromCart([FromQuery] int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == null) {
                return BadRequest("You need to be logged in.");
            }
            var result = _cartService.RemoveItemFromCart(userId, productId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}