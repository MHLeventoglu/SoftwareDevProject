using Business.Abstract.Users;
using Core.Utilities.Results;
using Entities.Concrete.Users;

namespace Business.Concrete.Users;

public class CustomerManager : ICustomerService
{
    public IResult Add(Customer entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Customer entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Customer entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Customer>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Customer> GetById(int id)
    {
        throw new NotImplementedException();
    }