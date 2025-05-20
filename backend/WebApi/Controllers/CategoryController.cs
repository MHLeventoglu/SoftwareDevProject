using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Category category)
        {
            _categoryService.Add(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest();

            var result = _categoryService.Update(category);
            if (!result.Success)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryToDelete = _categoryService.GetById(id);
            if (!categoryToDelete.Success || categoryToDelete.Data == null)
                return NotFound();

            var result = _categoryService.Delete(categoryToDelete.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

       
    }
}
