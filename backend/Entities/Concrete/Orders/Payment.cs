using Core.Entities;

namespace Entities.Concrete.Orders;

public class Payment:IEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Status { get; set; }
    public string? PaymentMethod { get; set; }
}