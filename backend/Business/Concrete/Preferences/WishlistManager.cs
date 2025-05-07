using Business.Abstract.Preferences;
using Core.Utilities.Results;
using Entities.Concrete.Preferences;

namespace Business.Concrete.Preferences
{
    public class WishlistManager : IWishlistService
    {
        public IResult Add(Wishlist entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Wishlist entity)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Wishlist entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Wishlist>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<Wishlist> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}