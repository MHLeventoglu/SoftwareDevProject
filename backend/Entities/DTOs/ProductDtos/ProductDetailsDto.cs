using Core.Entities;
using Entities.Concrete.Products;

namespace Entities.DTOs.ProductDtos;

public class ProductDetailsDto : IDto
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
    //public List<Review> Reviews { get; set; }
}
