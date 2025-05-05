using Core.DataAccess;
using Entities.Concrete.Preferences;

namespace DataAccess.Abstract.Preferences;

public interface IAddressDal : IEntityRepository<Address>
{
    // Additional methods specific to Address can be added here
}