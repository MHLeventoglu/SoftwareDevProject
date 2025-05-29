using Core.Utilities.Results;
using Entities.Concrete.Preferences;

namespace Business.Abstract.Preferences;

public interface IWishlistService : IBaseService<Wishlist>
{
    // Wishlist'e özel metotlar buraya eklenebilir
    IDataResult<Wishlist> GetByUserId(int userId);
}
