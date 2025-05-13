using Core.DataAccess;
using Entities.Concrete.Orders;

namespace DataAccess.Abstract.Orders;

public interface IOrderDal : IEntityRepository<Order>
{
    // Additional methods specific to Order can be added here
}