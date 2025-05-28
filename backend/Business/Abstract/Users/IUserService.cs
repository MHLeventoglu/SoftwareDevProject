using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract.Users
{
    public interface IUserService : IBaseService<User>
    {
        IResult SendVerificationEmail(string email);
        IResult VerifyEmail(string email, string token);
    }
}