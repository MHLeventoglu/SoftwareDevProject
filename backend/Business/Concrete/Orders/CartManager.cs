using Business.Abstract.Orders;
using Core.Utilities.Results;
using Entities.Concrete.Orders;

namespace Business.Concrete.Orders;

public class CartManager : ICartService
{
    public IResult Add(Cart entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Cart entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Cart entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Cart>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Cart> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
