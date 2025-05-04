using System.Linq.Expressions;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.InMemory;

public class InMemoryProductDal : IProductDal
{
    readonly List<Product> _products;
    public InMemoryProductDal()
    {
        _products = new List<Product>
        {
            new Product { Id = 1, ProductName = "Product1", Description = "Description1", BrandId = 1, CategoryId = 1, Price = 100, StockCount = 10, DateAdded = DateTime.Now },
            new Product { Id = 2, ProductName = "Product2", Description = "Description2", BrandId = 2, CategoryId = 2, Price = 200, StockCount = 20, DateAdded = DateTime.Now },
            new Product { Id = 3, ProductName = "Product3", Description = "Description3", BrandId = 3, CategoryId = 3, Price = 300, StockCount = 30, DateAdded = DateTime.Now }
        };
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Delete(Product product)
    {
        Product? productToDelete = _products.SingleOrDefault(p => p.Id == product.Id);
        if (productToDelete != null)
        {
            _products.Remove(productToDelete);
        }
    }

    public Product Get(Expression<Func<Product, bool>>? filter)
    {
        Func<Product, bool> compiledFilter = filter.Compile();
        return _products.SingleOrDefault(compiledFilter);
    }

    public List<Product> GetAll(Expression<Func<Product, bool>>? filter = null)
    {
        return filter == null ? _products : _products.Where(filter.Compile()).ToList();
    }

    public void Update(Product product)
    {
        Product? productToUpdate = _products.SingleOrDefault(p => p.Id == product.Id);
        if (productToUpdate != null)
        {
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Description = product.Description;
            productToUpdate.BrandId = product.BrandId;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.Price = product.Price;
            productToUpdate.StockCount = product.StockCount;
            productToUpdate.DateAdded = product.DateAdded;
        }
    }
}
