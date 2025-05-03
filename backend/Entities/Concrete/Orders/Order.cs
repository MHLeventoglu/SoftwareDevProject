using Entities.Abstract;

namespace Entities.Concrete.Orders;

public class Order:IEntity
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public string? Status { get; set; }
    public string? AddressId { get; set; }
}