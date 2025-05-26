using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _brandService.GetById(id);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Brand brand)
        {
            var result = _brandService.Add(brand);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _brandService.Update(brand);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brand = _brandService.GetById(id);
            if (!brand.Success || brand.Data == null)
                return NotFound("Marka bulunamadı.");

            var result = _brandService.Delete(brand.Data);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
