using Entities.Abstract;

namespace Entities.Concrete.Orders;

public class CartItem:IEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }
}