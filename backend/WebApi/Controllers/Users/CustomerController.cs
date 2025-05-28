using Business.Abstract.Users;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Customer customer)
        {
            var result = _customerService.Add(customer);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customerResult = _customerService.GetById(id);
            if (!customerResult.Success)
             return NotFound(customerResult.Message);

   
             var result = _customerService.Delete(customerResult.Data);
            if (!result.Success)
            return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
