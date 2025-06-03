using Business.Abstract.Users;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Customer customer)
        {
            var result = _customerService.Add(customer);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
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