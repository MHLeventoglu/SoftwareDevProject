using Business.Abstract.Orders;
using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;

public class CartManager : ICartService
{
    private readonly ICartDal _cartDal;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;

    public CartManager(ICartDal cartDal, IUserService userService, ICartItemService cartItemService)
    {
        _cartDal = cartDal;
        _userService = userService;
        _cartItemService = cartItemService;
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
        var cart = _cartDal.Get(c => c.Id == id && c.Status);
        if (cart == null)
            return new ErrorDataResult<Cart>("Cart not found.");
        return new SuccessDataResult<Cart>(cart, "Cart found.");
    }

    public IDataResult<Cart> GetCartByUserId(int id)
    {
        var cart = _cartDal.Get(c => c.CustomerId == id);
        if (cart == null)
            return new ErrorDataResult<Cart>("Cart not found for the specified user.");
        return new SuccessDataResult<Cart>(cart, "Cart found for the specified user.");
    }

    public IResult AddItemToCart(int userId, int productId, int quantity)
    {
        var userResult = _userService.GetById(userId);
        if (!userResult.Success || userResult.Data == null)
            return new ErrorResult("User not found.");
        var user = userResult.Data;
        if (user is not Entities.Concrete.Users.Customer customer)
            return new ErrorResult("User is not a customer.");
        var cartResult = GetCartByUserId(userId);
        Cart? cart = null;
        if (cartResult.Success && cartResult.Data != null)
        {
            cart = cartResult.Data;
        }
        else
        {
            var newResult = Add(new Cart
            {
                CustomerId = userId,
                Items = new List<CartItem>(),
                Status = true
            });
            if (!newResult.Success)
            {
                return new ErrorResult("Failed to create a new cart.");
            }
            cart = _cartDal.Get(c => c.CustomerId == userId && c.Status);
            if (cart == null)
                return new ErrorResult("Cart could not be loaded after creation.");
            if (cart.Items == null)
                cart.Items = new List<CartItem>();
        }

        var existingItem = cart.Items?.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            var updateResult = _cartItemService.Update(existingItem);
            if (!updateResult.Success)
                return new ErrorResult("Failed to update cart item.");
        }
        else
        {
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            };
            var addResult = _cartItemService.Add(cartItem);
            if (!addResult.Success)
                return new ErrorResult("Failed to add item to cart.");
            if (cart.Items == null)
                cart.Items = new List<CartItem>();
            cart.Items.Add(cartItem);
        }
        return new SuccessResult("Item added to cart successfully.");
    }

    public IResult RemoveItemFromCart(int userId, int productId)
    {
        var userResult = _userService.GetById(userId);
        if (!userResult.Success || userResult.Data == null)
            return new ErrorResult("User not found.");
        var user = userResult.Data;
        if (user is not Entities.Concrete.Users.Customer customer)
            return new ErrorResult("User is not a customer.");
        int? cartId = customer.ActiveCartId;
        if (!cartId.HasValue)
            return new ErrorResult("User does not have an active cart.");
        var cart = _cartDal.Get(c => c.Id == cartId.Value);
        if (cart == null)
            return new ErrorResult("Cart not found.");
        if (cart.Items == null)
            return new ErrorResult("Cart is empty.");
        var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove == null)
            return new ErrorResult("Product not found in cart.");
        cart.Items.Remove(itemToRemove);
        _cartDal.Update(cart);
        return new SuccessResult("Item removed from cart successfully.");
    }
}
