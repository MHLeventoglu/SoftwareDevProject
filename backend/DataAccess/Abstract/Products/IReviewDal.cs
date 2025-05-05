using Core.DataAccess;
using Entities.Concrete.Products;

namespace DataAccess.Abstract.Products;

public interface IReviewDal : IEntityRepository<Review>
{
    // Additional methods specific to Review can be added here
}