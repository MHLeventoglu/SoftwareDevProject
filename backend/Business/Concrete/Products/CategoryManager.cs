using Business.Abstract.Products;
using Core.Utilities.Results;
using Entities.Concrete.Products;

namespace Business.Concrete.Products;

public class CategoryManager : ICategoryService
{
    public IResult Add(Category entity) => throw new NotImplementedException();
    public IResult Delete(Category entity) => throw new NotImplementedException();
    public IResult Update(Category entity) => throw new NotImplementedException();
    public IDataResult<List<Category>> GetAll() => throw new NotImplementedException();
    public IDataResult<Category> GetById(int id) => throw new NotImplementedException();
}
