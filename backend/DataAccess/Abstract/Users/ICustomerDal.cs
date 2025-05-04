using Core.DataAccess;
using Entities.Concrete.Users;

namespace DataAccess.Abstract.Users;

public interface ICustomerDal : IEntityRepository<Customer>
{
    // Additional methods specific to Customer can be added here
}