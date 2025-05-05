using Core.DataAccess;
using Entities.Concrete.Products;

namespace DataAccess.Abstract.Products;

public interface IBrandDal : IEntityRepository<Brand>
{
    // Additional methods specific to Brand can be added here
}