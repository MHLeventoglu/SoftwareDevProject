using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.EntityFramework.Products;

public class EfBrandDal : EfEntityRepositoryBase<Brand, DataBaseContext>, IBrandDal
{
}
