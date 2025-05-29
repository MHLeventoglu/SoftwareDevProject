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

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _cartItemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _cartItemService.GetById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CartItem cartItem)
        {
            _cartItemService.Add(cartItem);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CartItem cartItem)
        {
            var result = _cartItemService.Update(cartItem);
            if (!result.Success)
            return NotFound(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartItemResult = _cartItemService.GetById(id);
            if (!cartItemResult.Success)
            return NotFound(cartItemResult.Message);

      
            var result = _cartItemService.Delete(cartItemResult.Data);
            if (!result.Success)
            return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
