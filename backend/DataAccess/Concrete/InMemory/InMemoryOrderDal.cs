using System.Linq.Expressions;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace DataAccess.Concrete.InMemory;

public class InMemoryOrderDal : IOrderDal
{
    readonly List<Order> _orders;

    public InMemoryOrderDal()
    {
        _orders = new List<Order>
        {
            new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, TotalAmount = 500 },
            new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, TotalAmount = 1000 },
            new Order { Id = 3, CustomerId = 3, OrderDate = DateTime.Now, TotalAmount = 1500 }
        };
    }

    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public void Delete(Order order)
    {
        Order? orderToDelete = _orders.SingleOrDefault(o => o.Id == order.Id);
        if (orderToDelete != null)
        {
            _orders.Remove(orderToDelete);
        }
    }

    public Order Get(Expression<Func<Order, bool>>? filter)
    {
        Func<Order, bool> compiledFilter = filter.Compile();
        return _orders.SingleOrDefault(compiledFilter);
    }

    public List<Order> GetAll(Expression<Func<Order, bool>>? filter = null)
    {
        return filter == null ? _orders : _orders.Where(filter.Compile()).ToList();
    }

    public void Update(Order order)
    {
        Order? orderToUpdate = _orders.SingleOrDefault(o => o.Id == order.Id);
        if (orderToUpdate != null)
        {
            orderToUpdate.CustomerId = order.CustomerId;
            orderToUpdate.OrderDate = order.OrderDate;
            orderToUpdate.TotalAmount = order.TotalAmount;
        }
    }
}