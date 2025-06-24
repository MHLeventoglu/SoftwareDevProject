using Business.Abstract.Users;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
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

  [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest();

            var result = _userService.Update(user);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var userResult = _userService.GetById(id);
            if (!userResult.Success || userResult.Data == null)
                return NotFound(userResult.Message);

            var result = _userService.Delete(userResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
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
