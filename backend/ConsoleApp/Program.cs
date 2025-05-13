// See https://aka.ms/new-console-template for more information
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Products;
using DataAccess.Abstract.Products;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete.Products;

// Ensure database is created
using (var context = new DataBaseContext())
{
    context.Database.Migrate();
}

IProductDal productDal = new EfProductDal();
ICategoryDal categoryDal = new EfCategoryDal();
IBrandDal brandDal = new EfBrandDal();
Console.WriteLine("Products in the database:");
// brandDal.Add(new Brand { BrandName = "Test Brand3" });
// categoryDal.Add(new Category { CategoryName = "Test Category3" });
// productDal.Add(new Product { ProductName = "Test Product3", Price = 10.0m, Description = "Test Description", StockCount = 100, BrandId = 2, CategoryId = 3 });
var products = productDal.GetAll();

foreach (var product in products)
{
    Console.WriteLine($"Product: {product.ProductName}");
}