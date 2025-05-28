using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract.Users
{
<<<<<<< HEAD
    public interface IUserService : IBaseService<User>
    {
        IResult SendVerificationEmail(string email);
        IResult VerifyEmail(string email, string token);
    }
}
=======
    // User'a özel metodlar buraya
    IDataResult<User> GetByEmail(string email);
    IDataResult<List<OperationClaim>> GetClaims(User user);
}
>>>>>>> 40c417d9e2a7e331912d36e32b93cb3f3adf8e23
