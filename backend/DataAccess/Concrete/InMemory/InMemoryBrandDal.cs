using System.Linq.Expressions;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.InMemory;

public class InMemoryBrandDal : IBrandDal
{
    readonly List<Brand> _brands;

    public InMemoryBrandDal()
    {
        _brands = new List<Brand>
        {
            new Brand { Id = 1, BrandName = "Brand1" },
            new Brand { Id = 2, BrandName = "Brand2" },
            new Brand { Id = 3, BrandName = "Brand3" }
        };
    }

    public void Add(Brand brand)
    {
        _brands.Add(brand);
    }

    public void Delete(Brand brand)
    {
        Brand? brandToDelete = _brands.SingleOrDefault(b => b.Id == brand.Id);
        if (brandToDelete != null)
        {
            _brands.Remove(brandToDelete);
        }
    }

    public Brand Get(Expression<Func<Brand, bool>>? filter)
    {
        Func<Brand, bool> compiledFilter = filter.Compile();
        return _brands.SingleOrDefault(compiledFilter);
    }

    public List<Brand> GetAll(Expression<Func<Brand, bool>>? filter = null)
    {
        return filter == null ? _brands : _brands.Where(filter.Compile()).ToList();
    }

    public void Update(Brand brand)
    {
        Brand? brandToUpdate = _brands.SingleOrDefault(b => b.Id == brand.Id);
        if (brandToUpdate != null)
        {
            brandToUpdate.BrandName = brand.BrandName;
        }
    }
}