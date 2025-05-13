using Core.DataAccess;
using Entities.Concrete.Users;

namespace DataAccess.Abstract.Users;

public interface IStaffDal : IEntityRepository<Staff>
{
    // Additional methods specific to Admin can be added here
}