using Business.Abstract.Users;
using Core.Utilities.Results;
using Entities.Concrete.Users;

namespace Business.Concrete.Users;

public class StaffManager : IStaffService
{
    public IResult Add(Staff entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Staff entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Staff entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Staff>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Staff> GetById(int id)
    {
        throw new NotImplementedException();
    }
}