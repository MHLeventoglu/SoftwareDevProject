using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface IPaymentService : IBaseService<Payment>
{
    // Payment'a özel metotlar varsa buraya ekle
}
