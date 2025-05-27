using Business.Abstract.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
          private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult StartPayment([FromBody] PaymentRequest request)
        {
            var result = _paymentService.StartPayment(request);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

       
    }
}
