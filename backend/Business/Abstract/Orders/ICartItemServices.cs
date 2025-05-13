using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface ICartItemService : IBaseService<CartItem>
{
    // CartItem'a özel metotlar varsa buraya ekle
}
