using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace DataAccess.Concrete.EntityFramework.Users;

public class EfCustomerDal : EfEntityRepositoryBase<Customer, DataBaseContext>, ICustomerDal
{
}
