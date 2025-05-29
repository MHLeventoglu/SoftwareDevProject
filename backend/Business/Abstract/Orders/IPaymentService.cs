using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface IPaymentService
{
    IResult StartPayment(PaymentRequest request);
    IDataResult<string> GetPaymentStatus(string id);
}
