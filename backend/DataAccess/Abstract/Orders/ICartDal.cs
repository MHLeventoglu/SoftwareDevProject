using Core.DataAccess;
using Entities.Concrete.Orders;

namespace DataAccess.Abstract.Orders;

public interface ICartDal : IEntityRepository<Cart>
{
    // Additional methods specific to Cart can be added here
}