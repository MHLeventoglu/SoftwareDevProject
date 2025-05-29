using Core.Utilities.Results;
using Entities.Concrete.Orders;
using Entities.DTOs.Orders;
using System.Collections.Generic;
using Business.Abstract;
namespace Business.Abstract.Orders
{
    public interface IOrderService : IBaseService<Order>
    {
        IDataResult<List<Order>> GetOrdersByUserId(int userId);
        IResult CreateOrder(OrderCreateDto dto);
    }
}
