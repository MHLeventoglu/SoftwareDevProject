using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Users;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Users;

public class EfUserDal : EfEntityRepositoryBase<User, DataBaseContext>, IUserDal
{
}
