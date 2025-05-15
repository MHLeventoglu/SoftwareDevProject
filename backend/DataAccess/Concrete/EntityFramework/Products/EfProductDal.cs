using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using Entities.DTOs.ProductDtos;

namespace DataAccess.Concrete.EntityFramework.Products;

public class EfProductDal : EfEntityRepositoryBase<Product, DataBaseContext>, IProductDal
{
    public List<ProductDetailsDto> GetProductDetails()
    {
        using (var context = new DataBaseContext())
        {
            var result = from p in context.Products
                         join c in context.Categories on p.CategoryId equals c.Id
                         join b in context.Brands on p.BrandId equals b.Id
                         select new ProductDetailsDto
                         {
                             Id = p.Id,
                             ProductName = p.ProductName,
                             Description = p.Description,
                             BrandName = b.BrandName,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             StockCount = p.StockCount,
                             AverageRating = p.AverageRating
                         };
            return result.ToList();
        }
    }
}
