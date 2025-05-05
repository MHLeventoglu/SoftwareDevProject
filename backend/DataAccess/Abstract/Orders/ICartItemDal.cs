using Core.DataAccess;
using Entities.Concrete.Orders;

namespace DataAccess.Abstract.Orders;

public interface ICartItemDal : IEntityRepository<CartItem>
{
    // Additional methods specific to CartItem can be added here
}