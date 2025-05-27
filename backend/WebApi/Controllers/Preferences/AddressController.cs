using Business.Abstract.Preferences;
using Entities.Concrete.Preferences;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{userId}")]
        public IActionResult GetAddressesByUserId(int userId)
        {
            var result = _addressService.GetByUserId(userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Address address)
        {
            var result = _addressService.Add(address);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var addressResult = _addressService.GetById(id);
            if (!addressResult.Success)
            return NotFound(addressResult.Message);

   
            var result = _addressService.Delete(addressResult.Data);
            if (!result.Success)
              return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
   
