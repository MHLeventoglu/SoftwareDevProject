using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract.Users;

public interface IUserDal : IEntityRepository<User>
{
    // Additional methods specific to User can be added here
    public List<OperationClaim> GetClaims(User user);

}