using Microsoft.AspNetCore.Mvc;
using Business.Abstract.Products;
using Entities.Concrete.Products;

namespace WebApi.Controllers.Products
{

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _productService.GetAll();
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _productService.GetById(id);
        if (result.Success)
            return Ok(result);
        return NotFound(result.Message);
    }

    [HttpPost("add")]
    public IActionResult Add(Product product)
    {
        var result = _productService.Add(product);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPut("update/{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest("Gönderilen ID ile ürün ID'si uyuşmuyor.");

        var result = _productService.Update(product);
        if (result.Success)
            return Ok(result);
        return BadRequest(result.Message);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var productResult = _productService.GetById(id);
        if (!productResult.Success || productResult.Data == null)
            return NotFound(productResult.Message);

        var result = _productService.Delete(productResult.Data);
        if (result.Success)
            return Ok(result);
        return BadRequest(result.Message);
    }
}

}