using Microsoft.AspNetCore.Mvc;
using Business.Abstract.Products;
using Entities.Concrete.Products;

namespace WebApi.Controllers
{

    [ApiController]//Controller'ın bir Web API olduğunu belirtir
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;//readonly Bu değişkenin sadece constructor içinde bir kez atanabileceğini, sonradan değiştirilemeyeceğini garanti eder.

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]// ASP.NET Core bu etiketi görüp ona göre davranır.Bu metot, HTTP GET isteklerine yanıt verecek
        public IActionResult GetAll()//tüm ürünleri businnes katmanından alıyor
        {
            var products = _productService.GetAll();
            return Ok(products); // 200 OK + JSON

        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound(); // 404

            return Ok(product);//products listesini HTTP 200 (OK) yanıtı ile birlikte JSON formatında kullanıcıya geri döndür.ok metodu verdiğim products nesnesini json formatına çevirip kullanıcıya gönderir.
        }

        // POST: api/product
        [HttpPost]
        public IActionResult Add([FromBody] Product product)//FromBody → JSON içeriğini alıp ProductDto nesnesine dönüştürür.
        {
            _productService.Add(product);
            return Ok(); // 200 OK
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            // kontrol: Route id ile gönderilen nesnedeki id uyuşuyor mu?
            if (id != product.Id)
                return BadRequest("Gönderilen ID ile ürün ID'si uyuşmuyor.");

            var result = _productService.Update(product);

            if (!result.Success)
                return NotFound();

            return Ok(result);
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productToDelete = _productService.GetById(id);

            if (!productToDelete.Success || productToDelete.Data == null)
                return NotFound();

            var result = _productService.Delete(productToDelete.Data);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }

}