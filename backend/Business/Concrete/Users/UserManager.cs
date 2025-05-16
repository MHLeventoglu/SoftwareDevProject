using Business.Abstract.Users;
using Core.Utilities.Results;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace Business.Concrete.Users;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(User entity)
    {
        _userDal.Add(entity);
        return new SuccessResult("User added successfully.");
    }

    public IResult Delete(User entity)
    {
        if (entity == null)
            return new ErrorResult("User not found.");

        _userDal.Delete(entity);
        return new SuccessResult("User deleted successfully.");
    }

    public IResult Update(User entity)
    {
        if (entity == null)
            return new ErrorResult("User not found.");

        _userDal.Update(entity);
        return new SuccessResult("User updated successfully.");
    }

    public IDataResult<List<User>> GetAll()
    {
        var users = _userDal.GetAll();
        return new SuccessDataResult<List<User>>(users);
    }

    public IDataResult<User> GetById(int id)
    {
        var user = _userDal.Get(u => u.Id == id);
        if (user == null)
            return new ErrorDataResult<User>("User not found.");

        return new SuccessDataResult<User>(user);
    }
}
