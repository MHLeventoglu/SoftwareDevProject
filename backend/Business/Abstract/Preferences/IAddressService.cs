using Core.Utilities.Results;
using Entities.Concrete.Preferences;

namespace Business.Abstract.Preferences;

public interface IAddressService : IBaseService<Address>
{
    // Address'e özel metotlar buraya eklenebilir
    IDataResult<Address> GetByUserId(int userId);
}
