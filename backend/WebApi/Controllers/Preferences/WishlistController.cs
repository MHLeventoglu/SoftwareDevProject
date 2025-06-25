using Business.Abstract.Preferences;
using Entities.Concrete.Preferences;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Preferences
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.User + "," + Roles.Admin)]
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
            // Add user verification
            var userIdClaim = User.FindFirst("nameid")?.Value;
            if (userIdClaim != userId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _wishlistService.GetByUserId(userId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Wishlist item)
        {
            var result = _wishlistService.Add(item);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete/{productId}")]
        public IActionResult Delete(int productId)
        {
            var wishlistResult = _wishlistService.GetById(productId);
            if (!wishlistResult.Success || wishlistResult.Data == null)
                return NotFound(wishlistResult);

            var result = _wishlistService.Delete(wishlistResult.Data);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}