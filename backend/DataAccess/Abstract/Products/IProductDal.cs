using Core.DataAccess;
using Entities.Concrete.Products;

namespace DataAccess.Abstract.Products;

public interface IProductDal : IEntityRepository<Product>
{
    // Additional methods specific to Product can be added here
}