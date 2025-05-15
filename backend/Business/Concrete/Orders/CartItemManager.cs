using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;
// bussiness layer for CartItem management
public class CartItemManager : ICartItemService
{
    public IResult Add(CartItem entity)
    {
        _cartItemDal.Add(entity);
        return new SuccessResult("Cart Item added Sucessfully");
    }

    public IResult Delete(CartItem entity)
    {
        if (entity == null)
        {
            return new ErrorResult("Cart Item not found");
        }
        _cartItemDal.Delete(entity);
    }

    public IResult Update(CartItem entity)
    {
        if (entity == null)    
        {
            return new ErrorResult("Cart Item not found");
        }
        _cartItemDal.Update(entity);
        return new SuccessResult("Cart Item updated successfully");
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
