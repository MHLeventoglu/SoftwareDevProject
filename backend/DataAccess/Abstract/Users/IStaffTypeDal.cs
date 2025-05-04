using Core.DataAccess;
using Entities.Concrete.Users;

namespace DataAccess.Abstract.Users;

public interface IStaffTypeDal : IEntityRepository<StaffType>
{
    // Additional methods specific to StaffType can be added here
}