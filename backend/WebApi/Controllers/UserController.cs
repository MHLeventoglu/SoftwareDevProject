using Business.Abstract.Users;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var result = _userService.Register(user);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _userService.Update(user);
            if (!result.Success)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("send-verification-email")]
        public IActionResult SendVerificationEmail([FromBody] string email)
        {
            var result = _userService.SendVerificationEmail(email);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("verify-email")]
        public IActionResult VerifyEmail([FromQuery] string email, [FromQuery] string token)
        {
            var result = _userService.VerifyEmail(email, token);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
