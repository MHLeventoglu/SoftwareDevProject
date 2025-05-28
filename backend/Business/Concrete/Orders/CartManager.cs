using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;
using DataAccess.Abstract.Orders;


namespace Business.Concrete.Orders;

public class CartManager : ICartService
{
    private readonly ICartDal _cartDal;

    public CartManager(ICartDal cartDal)
    {
        _cartDal = cartDal;
    }

    public IResult Add(Cart entity)
    {
        _cartDal.Add(entity);
        return new SuccessResult("Cart added successfully.");
    }

    public IResult Delete(Cart entity)
    {
        if (entity == null)
        {
            return new ErrorResult("Cart not found.");
        }

        _cartDal.Delete(entity);
        return new SuccessResult("Cart deleted successfully.");
    }

    public IResult Update(Cart entity)
    {
        var cart = _cartDal.Get(c => c.Id == entity.Id);
        if (cart == null)
            return new ErrorResult("Cart not found.");
        // Update properties as needed
        cart.Id = entity.Id;
        cart.Items = entity.Items;
        // ...add other property updates if needed...
        return new SuccessResult("Cart updated successfully.");
    }

    public IDataResult<List<Cart>> GetAll()
    {
        return new SuccessDataResult<List<Cart>>(_cartDal.GetAll(), "Carts listed successfully.");
    }

    public IDataResult<Cart> GetById(int id)
    {
        var cart = _cartDal.Get(c => c.Id == id);
        if (cart == null)
            return new ErrorDataResult<Cart>("Cart not found.");
        return new SuccessDataResult<Cart>(cart, "Cart found.");
    }
}
