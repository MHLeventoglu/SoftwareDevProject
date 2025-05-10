using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.EntityFramework.Products;

public class EfProductDal : EfEntityRepositoryBase<Product, DataBaseContext>, IProductDal
{
    // Eğer özel bir sorgu gerekiyorsa burada implement edebilirsin
}
