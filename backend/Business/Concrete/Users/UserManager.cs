using Business.Abstract.Users;
using Core.Utilities.Results;
using Entities.Concrete.Users;

namespace Business.Concrete.Users;

public class UserManager : IUserService
{
    public IResult Add(User entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(User entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<User> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
