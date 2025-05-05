using Core.DataAccess;
using Entities.Concrete.Users;

namespace DataAccess.Abstract.Users;

public interface IUserDal : IEntityRepository<User>
{
    // Additional methods specific to User can be added here
}