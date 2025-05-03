using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace  Business.Concrete.Orders;

public class OrderManager:IOrderService
{
    public IResult Add(Order entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Order entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Order entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Order>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Order> GetById(int id)
    {
        throw new NotImplementedException();
    }
}

