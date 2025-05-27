using Business.Abstract.Users;
using Entities.Concrete.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffTypeController : ControllerBase
    {
         private readonly IStaffTypeService _staffTypeService;

        public StaffTypeController(IStaffTypeService staffTypeService)
        {
            _staffTypeService = staffTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _staffTypeService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] StaffType type)
        {
            var result = _staffTypeService.Add(type);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var staffTypeResult = _staffTypeService.GetById(id);
            if (!staffTypeResult.Success)
                return BadRequest(staffTypeResult.Message);

            var result = _staffTypeService.Delete(staffTypeResult.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
