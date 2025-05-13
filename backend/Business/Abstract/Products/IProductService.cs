using Entities.Concrete.Products;

namespace Business.Abstract.Products;

public interface IProductService : IBaseService<Product>
{
    // Product'a özel metotlar buraya yazılabilir
}
