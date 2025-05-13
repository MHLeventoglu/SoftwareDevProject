using Business.Abstract.Products;
using Core.Utilities.Results;
using Entities.Concrete.Products;

namespace Business.Concrete.Products;

public class DiscountManager : IDiscountService
{
    public IResult Add(Discount entity) => throw new NotImplementedException();
    public IResult Delete(Discount entity) => throw new NotImplementedException();
    public IResult Update(Discount entity) => throw new NotImplementedException();
    public IDataResult<List<Discount>> GetAll() => throw new NotImplementedException();
    public IDataResult<Discount> GetById(int id) => throw new NotImplementedException();
}
