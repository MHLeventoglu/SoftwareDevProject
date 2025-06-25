using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs.UserDtos;
using System.Collections.Generic;

namespace Business.Abstract.Users
{
    public interface IUserService : IBaseService<User>
    {
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Register(UserForRegisterDto dto);
        IResult SendVerificationEmail(string email,string token);
        IResult VerifyEmail(string email, string verificationCode);
        IResult SendNotification(string email, string message);
        IResult AssignRole(int userId, string role);
    }
}
