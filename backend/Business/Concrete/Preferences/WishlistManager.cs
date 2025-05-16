using Business.Abstract.Preferences;
using Core.Utilities.Results;
using DataAccess.Abstract.Preferences;
using Entities.Concrete.Preferences;
using System.Collections.Generic;

namespace Business.Concrete.Preferences
{
    public class WishlistManager : IWishlistService
    {
        private readonly IWishlistDal _wishlistDal;

        public WishlistManager(IWishlistDal wishlistDal)
        {
            _wishlistDal = wishlistDal;
        }

        public IResult Add(Wishlist entity)
        {
            _wishlistDal.Add(entity);
            return new SuccessResult("Wishlist added successfully.");
        }

        public IResult Delete(Wishlist entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Wishlist not found.");
            }
            _wishlistDal.Delete(entity);
            return new SuccessResult("Wishlist deleted successfully.");
        }

        public IResult Update(Wishlist entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Wishlist not found.");
            }
            _wishlistDal.Update(entity);
            return new SuccessResult("Wishlist updated successfully.");
        }

        public IDataResult<List<Wishlist>> GetAll()
        {
            var wishlists = _wishlistDal.GetAll();
            return new SuccessDataResult<List<Wishlist>>(wishlists);
        }

        public IDataResult<Wishlist> GetById(int id)
        {
            var wishlist = _wishlistDal.Get(w => w.Id == id);
            if (wishlist == null)
            {
                return new ErrorDataResult<Wishlist>("Wishlist not found.");
            }
            return new SuccessDataResult<Wishlist>(wishlist);
        }
    }
}
