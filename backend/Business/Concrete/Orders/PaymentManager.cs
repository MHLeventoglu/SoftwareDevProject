using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;

public class PaymentManager : IPaymentService
{
    public IResult Add(Payment entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Payment entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Payment entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Payment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Payment> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
