using Business.Abstract.Orders;
using Entities.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Orders
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

        [HttpPost("start")]
        public IActionResult StartPayment([FromBody] PaymentRequest request)
        {
            var result = _paymentService.StartPayment(request);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}