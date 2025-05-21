using Core.Entities;

namespace Entities.DTOs.ProductDtos;

public class ProductListDto : IDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public short StockCount { get; set; }
}
