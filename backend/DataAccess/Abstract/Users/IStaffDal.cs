using Core.DataAccess;
using Entities.Concrete.Users;

namespace DataAccess.Abstract.Users;

public interface IAdminDal : IEntityRepository<Staff>
{
    // Additional methods specific to Admin can be added here
}