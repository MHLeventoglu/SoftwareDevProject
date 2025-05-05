using Core.Entities;

namespace Entities.Concrete.Orders;

public class Order:IEntity
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public string? Status { get; set; }
    public string? AddressId { get; set; }
}