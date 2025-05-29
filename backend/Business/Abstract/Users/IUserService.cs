using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs.UserDtos;
using System.Collections.Generic;

namespace Business.Abstract.Users
{
    public interface IUserService
    {
        IResult Add(User entity);
        IResult Update(User entity);
        IResult Delete(User entity);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetById(int id);
        IResult Register(UserForRegisterDto dto);
        IResult SendVerificationEmail(string email);
        IResult VerifyEmail(string email, string code);
    }
}
