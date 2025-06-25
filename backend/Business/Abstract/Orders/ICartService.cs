using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface ICartService : IBaseService<Cart>
{
    // Cart'a özel metotlar varsa buraya ekle
    IDataResult<Cart> GetCartByUserId(int id);
    IResult AddItemToCart(int userID, int productId, int quantity);
    IResult RemoveItemFromCart(int userID, int productId);

}
