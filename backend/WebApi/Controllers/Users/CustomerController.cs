using Business.Abstract.Users;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetById(int id)
        {
            // Add user verification
            var userIdClaim = User.FindFirst("nameid")?.Value;
            if (userIdClaim != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var result = _customerService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add([FromBody] Customer customer)
        {
            var result = _customerService.Add(customer);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Update(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _customerService.Update(customer);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            var customerResult = _customerService.GetById(id);
            if (!customerResult.Success || customerResult.Data == null)
                return NotFound(customerResult.Message);

            var result = _customerService.Delete(customerResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}