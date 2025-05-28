using Business.Abstract.Orders;
using Core.Utilities.Results;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;
using DataAccess.Abstract.Orders;
using DataAccess.Abstract.Carts;

namespace  Business.Concrete.Orders;

public class OrderManager:IOrderService
{
    private readonly IOrderDal _orderDal;
    public OrderManager(IOrderDal orderDal)
    {
        _orderDal = orderDal;
    }
    public IResult Add(Order entity)
    {
        _orderDal.Add(entity);
        return new SuccessResult("Order added successfully.");
    }

    public IResult Delete(Order entity)
    {
        if (entity == null)
            return new ErrorResult("Order not found.");

        _orderDal.Delete(entity);
        return new SuccessResult("Order deleted successfully.");
    }

    public IResult Update(Order entity)
    {
        if (entity == null)
            return new ErrorResult("Order not found.");

        _orderDal.Update(entity);
        return new SuccessResult("Order updated successfully.");
    }

    public IDataResult<List<Order>> GetAll()
    {
        var result = _orderDal.GetAll();
        return new SuccessDataResult<List<Order>>(result, "Orders listed successfully.");
    }

    public IDataResult<Order> GetById(int id)
    {
        var order = _orderDal.Get(o => o.Id == id);
        if (order == null)
            return new ErrorDataResult<Order>("Order not found.");

        return new SuccessDataResult<Order>(order, "Order fetched successfully.");
    }
}


