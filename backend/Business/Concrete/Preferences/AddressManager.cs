using Business.Abstract.Preferences;
using Core.Utilities.Results;
using Entities.Concrete.Preferences;
using DataAccess.Abstract.Preferences;
using Entities.Concrete.Preferences;
using System.Collections.Generic;

namespace Business.Concrete.Preferences;

public class AddressManager : IAddressService
{
    private readonly IAddressDal _addressDal;

    public AddressManager(IAddressDal addressDal)
    {
        _addressDal = addressDal;
    }
    public IResult Add(Address entity)
    {
        _addressDal.Add(entity);
        return new SuccessResult("Address added successfully.");
    }

    public IResult Delete(Address entity)
    {
        if (entity == null)
        {
            return new ErrorResult("Address not found.");
        }
        _addressDal.Delete(entity);
        return new SuccessResult("Address deleted successfully.");
    }

    public IResult Update(Address entity)
    {
        if (entity == null)
        {
            return new ErrorResult("Address not found.");
        }
        _addressDal.Update(entity);
        return new SuccessResult("Address updated successfully.");
    }

    public IDataResult<List<Address>> GetAll()
    {
        var addresses = _addressDal.GetAll();
        return new SuccessDataResult<List<Address>>(addresses);
    }

    public IDataResult<Address> GetById(int id)
    {
        var address = _addressDal.Get(a => a.Id == id);
        if (address == null)
        {
            return new ErrorDataResult<Address>("Address not found.");
        }
        return new SuccessDataResult<Address>(address);
    }
}

