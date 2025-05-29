using System.Net;
using System.Net.Mail;
using Business.Abstract.Users;
using Core.Utilities.Results;
using Core.Entities.Concrete;
using DataAccess.Abstract.Users;
using Core.Entities.Concrete;
using Entities.DTOs.UserDtos;

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

    public IDataResult<User> GetByEmail(string email)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email), "User found by email.");
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var claims = _userDal.GetClaims(user);
        if (claims == null || claims.Count == 0)
            return new ErrorDataResult<List<OperationClaim>>("No claims found for this user.");

        return new SuccessDataResult<List<OperationClaim>>(claims, "Claims retrieved successfully.");
    }

    public IResult Register(UserForRegisterDto dto)
    {
        throw new NotImplementedException();
    }

    public IResult SendVerificationEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IResult VerifyEmail(string email, string verificationCode)
    {
        throw new NotImplementedException();
    }
}
