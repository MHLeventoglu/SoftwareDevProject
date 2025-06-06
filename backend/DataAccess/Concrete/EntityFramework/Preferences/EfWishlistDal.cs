using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.Preferences;
using Entities.Concrete.Preferences;

namespace DataAccess.Concrete.EntityFramework.Preferences;

public class EfWishlistDal : EfEntityRepositoryBase<Wishlist, DataBaseContext>, IWishlistDal
{
}
