using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace DataAccess.Concrete.EntityFramework.Users;

public class EfStaffTypeDal : EfEntityRepositoryBase<StaffType, DataBaseContext>, IStaffTypeDal
{
}
