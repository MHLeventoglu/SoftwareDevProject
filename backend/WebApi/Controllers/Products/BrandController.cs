using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Brand brand)
        {
            var result = _brandService.Add(brand);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
                return BadRequest();

            var result = _brandService.Update(brand);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var brandResult = _brandService.GetById(id);
            if (!brandResult.Success || brandResult.Data == null)
                return NotFound(brandResult.Message);

            var result = _brandService.Delete(brandResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
