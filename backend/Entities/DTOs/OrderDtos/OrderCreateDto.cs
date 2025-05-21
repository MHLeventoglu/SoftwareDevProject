using Core.Entities;

namespace Entities.DTOs.Orders;

public class OrderCreateDto : IDto
{
    public int CustomerId { get; set; }
    public string AddressId { get; set; }
    public string CouponCode { get; set; }
    public int CartId { get; set; }
    public PaymentCreateDto Payment { get; set; }
}

public class PaymentCreateDto
{
    public string PaymentMethod { get; set; }
    public double Amount { get; set; }
}
