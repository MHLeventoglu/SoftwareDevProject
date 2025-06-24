using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Category category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest();

            var result = _categoryService.Update(category);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var categoryResult = _categoryService.GetById(id);
            if (!categoryResult.Success || categoryResult.Data == null)
                return NotFound(categoryResult.Message);

            var result = _categoryService.Delete(categoryResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
