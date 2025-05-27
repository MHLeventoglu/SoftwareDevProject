using Business.Abstract.Users;
using Core.Entities.Concrete;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetById(id);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user)
        {
            var result = _userService.Add(user);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _userService.Update(user);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userResult = _userService.GetById(id);
            if (!userResult.Success)
                return NotFound(userResult.Message);

            var result = _userService.Delete(userResult.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
