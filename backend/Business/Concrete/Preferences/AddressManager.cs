using Business.Abstract.Preferences;
using Core.Utilities.Results;
using Entities.Concrete.Preferences;

namespace Business.Concrete.Preferences;

public class AddressManager : IAddressService
{
    public IResult Add(Address entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Address entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Address entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Address>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Address> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
