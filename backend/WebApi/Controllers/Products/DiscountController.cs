using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
         private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _discountService.GetAll();
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _discountService.GetById(id);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Discount discount)
        {
            var result = _discountService.Add(discount);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Discount discount)
        {
            if (id != discount.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = _discountService.Update(discount);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var discount = _discountService.GetById(id);
            if (!discount.Success || discount.Data == null)
                return NotFound("İndirim bulunamadı.");

            var result = _discountService.Delete(discount.Data);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
