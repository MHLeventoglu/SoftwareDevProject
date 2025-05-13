using Core.Entities;
using Entities.Concrete.Orders;

namespace Entities.DTOs.Orders;

public class OrderDetailDto : IDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; }
    public string AddressId { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public Payment Payment { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
