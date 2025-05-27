using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Users;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Users;

public class EfUserDal : EfEntityRepositoryBase<User, DataBaseContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new DataBaseContext())
        {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim() { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
        }
    }
}
