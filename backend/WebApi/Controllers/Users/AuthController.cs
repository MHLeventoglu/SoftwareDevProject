
using Business.Abstract;
using Entities.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLoginDto loginDto)
    {
        var result = _authService.Login(loginDto);
        if (!result.Success)
            return Unauthorized(result.Message);

        return Ok(result);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserForRegisterDto registerDto)
    {
        var result = _authService.Register(registerDto, registerDto.Password);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }
    }
}
