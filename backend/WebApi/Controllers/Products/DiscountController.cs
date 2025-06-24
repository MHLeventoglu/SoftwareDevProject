using Business.Abstract.Products;
using Entities.Concrete.Products;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
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

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _discountService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _discountService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Discount discount)
        {
            var result = _discountService.Add(discount);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Discount discount)
        {
            if (id != discount.Id)
                return BadRequest("Gönderilen ID ile indirim ID'si uyuşmuyor.");

            var result = _discountService.Update(discount);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var discountResult = _discountService.GetById(id);
            if (!discountResult.Success || discountResult.Data == null)
                return NotFound(discountResult.Message);

            var result = _discountService.Delete(discountResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
