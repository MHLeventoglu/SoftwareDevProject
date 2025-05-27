using Microsoft.AspNetCore.Mvc;
using Business.Abstract.Products;
using Entities.Concrete.Products;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _reviewService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _reviewService.GetById(id);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] Review review)
    {
        var result = _reviewService.Add(review);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Review review)
    {
        if (id != review.Id)
            return BadRequest();
            
        var result = _reviewService.Update(review);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var review = _reviewService.GetById(id);
        if (!review.Success)
            return NotFound();

        var result = _reviewService.Delete(review.Data);
        return Ok(result);
    }
}