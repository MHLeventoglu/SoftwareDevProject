using Core.Entities;

namespace Entities.Concrete.Products;

public class Brand:IEntity
{
    public int Id { get; set; }
    public string? BrandName { get; set; }
}