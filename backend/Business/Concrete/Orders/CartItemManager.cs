using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;

public class CartItemManager : ICartItemService
{
    public IResult Add(CartItem entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(CartItem entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(CartItem entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<CartItem>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<CartItem> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
