using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Users;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Users;

public class EfUserDal : EfEntityRepositoryBase<User, DataBaseContext>, IUserDal
{
    public void AddUserClaim(User user, OperationClaim claim)
    {
        using (var context = new DataBaseContext())
        {
            var existingClaim = context.OperationClaims.FirstOrDefault(c => c.Name == claim.Name);
            if (existingClaim == null)
            {
                // Create new claim if it doesn't exist
                existingClaim = new OperationClaim { Name = claim.Name };
                context.OperationClaims.Add(existingClaim);
                context.SaveChanges();
            }

            // Add user-claim relationship if it doesn't exist
            if (!context.UserOperationClaims.Any(uc => 
                uc.UserId == user.Id && uc.OperationClaimId == existingClaim.Id))
            {
                context.UserOperationClaims.Add(new UserOperationClaim
                {
                    UserId = (int)user.Id,
                    OperationClaimId = existingClaim.Id
                });
                context.SaveChanges();
            }
        }
    }

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
