using Business.Abstract.Preferences;
using Entities.Concrete.Preferences;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Preferences
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("getbyuserid/{userId}")]
        public IActionResult GetAddressesByUserId(int userId)
        {
            var result = _addressService.GetByUserId(userId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Address address)
        {
            var result = _addressService.Add(address);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var addressResult = _addressService.GetById(id);
            if (!addressResult.Success || addressResult.Data == null)
                return NotFound(addressResult);

            var result = _addressService.Delete(addressResult.Data);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}

