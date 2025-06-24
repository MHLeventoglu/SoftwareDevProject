using Microsoft.AspNetCore.Mvc;
using Business.Abstract.Products;
using Entities.Concrete.Products;

namespace WebApi.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _reviewService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _reviewService.GetById(id);
            if (result.Success)
                return Ok(result);
            return NotFound(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Review review)
        {
            var result = _reviewService.Add(review);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] Review review)
        {
            if (id != review.Id)
                return BadRequest();

            var result = _reviewService.Update(review);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var reviewResult = _reviewService.GetById(id);
            if (!reviewResult.Success || reviewResult.Data == null)
                return NotFound(reviewResult.Message);

            var result = _reviewService.Delete(reviewResult.Data);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}