using Business.Abstract.Users;
using Entities.Concrete.Users;
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

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _staffTypeService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _staffTypeService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] StaffType type)
        {
            var result = _staffTypeService.Add(type);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] StaffType type)
        {
            if (id != type.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _staffTypeService.Update(type);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var staffTypeResult = _staffTypeService.GetById(id);
            if (!staffTypeResult.Success || staffTypeResult.Data == null)
                return NotFound(staffTypeResult.Message);

            var result = _staffTypeService.Delete(staffTypeResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
