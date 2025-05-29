using Business.Abstract.Orders;
using Core.Utilities.Results;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;
using Entities.DTOs.OrderDtos; // Bunu eklemeyi unutma

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

    public IResult StartPayment(PaymentRequest request)
    {
        // Sadece mevcut alanlara göre örnek kayıt oluşturuluyor
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Status = "Processing"
        };

        _paymentDal.Add(payment);
        return new SuccessResult("Ödeme başlatıldı.");
    }

    public IDataResult<string> GetPaymentStatus(string id)
    {
        if (!int.TryParse(id, out var paymentId))
            return new ErrorDataResult<string>("Geçersiz ödeme ID.");

        var payment = _paymentDal.Get(p => p.Id == paymentId);
        if (payment == null)
            return new ErrorDataResult<string>("Ödeme bulunamadı.");

        return new SuccessDataResult<string>(payment.Status ?? "Durum bilinmiyor");
    }
}