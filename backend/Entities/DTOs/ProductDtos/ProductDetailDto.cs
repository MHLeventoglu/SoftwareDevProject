using Core.Entities;

namespace Entities.DTOs.ProductDtos;

public class ProductDetailDto : IDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public decimal Price { get; set; }
    public short StockCount { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}
