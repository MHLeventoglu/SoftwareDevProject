using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework.Orders;

public class EfCartDal : EfEntityRepositoryBase<Cart, DataBaseContext>, ICartDal
{
    public new Cart? Get(Expression<Func<Cart, bool>>? filter)
    {
        using (var context = new DataBaseContext())
        {
            return filter == null
                ? context.Carts.Include(c => c.Items).FirstOrDefault()
                : context.Carts.Include(c => c.Items).FirstOrDefault(filter);
        }
    }

    public new List<Cart> GetAll(Expression<Func<Cart, bool>>? filter = null)
    {
        using (var context = new DataBaseContext())
        {
            return filter == null
                ? context.Carts.Include(c => c.Items).ToList()
                : context.Carts.Include(c => c.Items).Where(filter).ToList();
        }
    }
}
