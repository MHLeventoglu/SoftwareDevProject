using Entities.Concrete.Orders;
using Core.Utilities.Results;
using Entities.DTOs.OrderDtos;

namespace Business.Abstract.Orders;

public interface IOrderService:IBaseService<Order>
{
        IDataResult<List<Order>> GetOrdersByUserId(int userId);
        IResult CreateOrder(OrderCreateDto dto);
}