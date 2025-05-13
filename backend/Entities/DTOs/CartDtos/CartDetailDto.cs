using Core.Entities;
using Entities.Concrete.Orders;

namespace Entities.DTOs.CartDtos;

public class CartDetailDto : IDto
{
    public int Id { get; set; }
    public List<CartItemDto> Items { get; set; }
    public string CouponCode { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountedAmount { get; set; }
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
