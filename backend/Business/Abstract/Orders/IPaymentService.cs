using Core.Utilities.Results;
using Entities.Concrete.Orders;
using Entities.DTOs.OrderDtos;

namespace Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.DTOs.OrderDtos;

public interface IPaymentService
{
    IResult StartPayment(PaymentRequest request);
    IDataResult<string> GetPaymentStatus(string id);
}
