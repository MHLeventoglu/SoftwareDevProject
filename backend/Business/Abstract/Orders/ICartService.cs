using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface ICartService : IBaseService<Cart>
{
    // Cart'a özel metotlar varsa buraya ekle
    IDataResult<Cart> GetCartByUserId(int id);
}
