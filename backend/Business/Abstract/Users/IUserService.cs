using Entities.Concrete.Users;

namespace Business.Abstract.Users;

public interface IUserService : IBaseService<User>
{
    // User'a özel metodlar buraya
}
