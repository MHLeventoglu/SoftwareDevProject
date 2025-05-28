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
            var updated = _cartItemService.Update(id, cartItem);
            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _cartItemService.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}
