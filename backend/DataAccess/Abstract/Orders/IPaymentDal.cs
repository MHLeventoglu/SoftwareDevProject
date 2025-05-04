using Core.DataAccess;
using Entities.Concrete.Orders;

namespace DataAccess.Abstract.Orders;

public interface IPaymentDal : IEntityRepository<Payment>
{
    // Additional methods specific to Payment can be added here
}