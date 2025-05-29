using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Abstract.Orders;

public interface IOrderService : IBaseService<Order>
{
    // Eğer bu classa özel bir metod tanımlayacaksan burada tanımla
    IDataResult<List<Order>> GetOrdersByUserId(int userId);
}