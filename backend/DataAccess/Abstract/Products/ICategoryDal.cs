using Core.DataAccess;
using Entities.Concrete.Products;

namespace DataAccess.Abstract.Products;

public interface ICategoryDal : IEntityRepository<Category>
{
    // Additional methods specific to Category can be added here
}