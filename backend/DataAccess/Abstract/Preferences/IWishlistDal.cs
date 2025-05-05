using Core.DataAccess;
using Entities.Concrete.Preferences;

namespace DataAccess.Abstract.Preferences;

public interface IWishlistDal : IEntityRepository<Wishlist>
{
    // Additional methods specific to Wishlist can be added here
}