using System.Linq.Expressions;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.InMemory;

public class InMemoryCategoryDal : ICategoryDal
{
    readonly List<Category> _categories;

    public InMemoryCategoryDal()
    {
        _categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Category1" },
            new Category { Id = 2, CategoryName = "Category2" },
            new Category { Id = 3, CategoryName = "Category3" }
        };
    }

    public void Add(Category category)
    {
        _categories.Add(category);
    }

    public void Delete(Category category)
    {
        Category? categoryToDelete = _categories.SingleOrDefault(c => c.Id == category.Id);
        if (categoryToDelete != null)
        {
            _categories.Remove(categoryToDelete);
        }
    }

    public Category Get(Expression<Func<Category, bool>>? filter)
    {
        Func<Category, bool> compiledFilter = filter.Compile();
        return _categories.SingleOrDefault(compiledFilter);
    }

    public List<Category> GetAll(Expression<Func<Category, bool>>? filter = null)
    {
        return filter == null ? _categories : _categories.Where(filter.Compile()).ToList();
    }

    public void Update(Category category)
    {
        Category? categoryToUpdate = _categories.SingleOrDefault(c => c.Id == category.Id);
        if (categoryToUpdate != null)
        {
            categoryToUpdate.CategoryName = category.CategoryName;
        }
    }
}