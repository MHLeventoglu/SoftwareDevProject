using Entities.Abstract;

namespace Entities.Concrete.Orders;

public class Cart:IEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<CartItem>? Items { get; set; }
    public string? CouponCode { get; set; }
    public bool Status { get; set; }
}