using Business.Abstract.Orders;
using Core.Utilities.Results;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;

public class PaymentManager : IPaymentService
{
    private readonly IPaymentDal _paymentDal;

    public PaymentManager(IPaymentDal paymentDal)
    {
        _paymentDal = paymentDal;
    }

    public IResult Add(Payment entity)
    {
        _paymentDal.Add(entity);
        return new SuccessResult("Payment added successfully.");
    }

    public IResult Delete(Payment entity)
    {
        if (entity == null)
            return new ErrorResult("Payment not found.");

        _paymentDal.Delete(entity);
        return new SuccessResult("Payment deleted successfully.");
    }

    public IResult Update(Payment entity)
    {
        if (entity == null)
            return new ErrorResult("Payment not found.");

        _paymentDal.Update(entity);
        return new SuccessResult("Payment updated successfully.");
    }

    public IDataResult<List<Payment>> GetAll()
    {
        var payments = _paymentDal.GetAll();
        return new SuccessDataResult<List<Payment>>(payments, "Payments listed successfully.");
    }

    public IDataResult<Payment> GetById(int id)
    {
        var payment = _paymentDal.Get(p => p.Id == id);
        if (payment == null)
            return new ErrorDataResult<Payment>("Payment not found.");

        return new SuccessDataResult<Payment>(payment, "Payment fetched successfully.");
    }
}
