using Business.Abstract.Preferences;
using Entities.Concrete.Preferences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Preferences
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
         private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _wishlistService.GetByUserId(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Wishlist item)
        {
            var result = _wishlistService.Add(item);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            var result = _wishlistService.Delete(productId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
