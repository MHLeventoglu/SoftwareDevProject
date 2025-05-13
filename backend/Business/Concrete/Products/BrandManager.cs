using Business.Abstract.Products;
using Core.Utilities.Results;
using Entities.Concrete.Products;

namespace Business.Concrete.Products;

public class BrandManager : IBrandService
{
    public IResult Add(Brand entity) => throw new NotImplementedException();
    public IResult Delete(Brand entity) => throw new NotImplementedException();
    public IResult Update(Brand entity) => throw new NotImplementedException();
    public IDataResult<List<Brand>> GetAll() => throw new NotImplementedException();
    public IDataResult<Brand> GetById(int id) => throw new NotImplementedException();
}
