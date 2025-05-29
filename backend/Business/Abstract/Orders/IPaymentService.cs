using Core.Utilities.Results;
using Entities.Concrete.Orders;
using Entities.DTOs.OrderDtos;

namespace Business.Abstract.Orders;

public interface IPaymentService
{
    IResult StartPayment(PaymentRequest request);
    IDataResult<string> GetPaymentStatus(string id);
}
