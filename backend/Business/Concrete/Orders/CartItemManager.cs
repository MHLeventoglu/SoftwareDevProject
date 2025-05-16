using Business.Abstract.Orders;
using Core.Utilities.Results;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders
{
    // Business layer for CartItem management
    public class CartItemManager : ICartItemService
    {
        private readonly ICartItemDal _cartItemDal;

        public CartItemManager(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }

        public IResult Add(CartItem entity)
        {
            _cartItemDal.Add(entity);
            return new SuccessResult("Cart item added successfully.");
        }

        public IResult Delete(CartItem entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Cart item not found.");
            }

            _cartItemDal.Delete(entity);
            return new SuccessResult("Cart item deleted successfully.");
        }

        public IResult Update(CartItem entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Cart item not found.");
            }

            _cartItemDal.Update(entity);
            return new SuccessResult("Cart item updated successfully.");
        }

        public IDataResult<List<CartItem>> GetAll()
        {
            var result = _cartItemDal.GetAll();
            return new SuccessDataResult<List<CartItem>>(result, "Cart items listed successfully.");
        }

        public IDataResult<CartItem> GetById(int id)
        {
            var result = _cartItemDal.Get(c => c.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<CartItem>("Cart item not found.");
            }

            return new SuccessDataResult<CartItem>(result, "Cart item retrieved successfully.");
        }
    }
}
