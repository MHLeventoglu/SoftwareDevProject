using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract.Users;

public interface IUserService : IBaseService<User>
{
    // User'a özel metodlar buraya
    IDataResult<User> GetByEmail(string email);
    IDataResult<List<OperationClaim>> GetClaims(User user);
}
