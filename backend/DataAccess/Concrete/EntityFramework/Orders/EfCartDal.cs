using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace DataAccess.Concrete.EntityFramework.Orders;

public class EfCartDal : EfEntityRepositoryBase<Cart, DataBaseContext>, ICartDal
{
}
