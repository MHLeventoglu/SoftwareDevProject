using Core.Entities;

namespace Entities.Concrete.Products;

public class Product:IEntity
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; } = "No description available.";
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public double AverageRating { get; set; } = 0.0;
    public short StockCount { get; set; }
    public DateTime? DateAdded { get; set; } = DateTime.Now;
}