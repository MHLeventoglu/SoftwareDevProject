using Core.DataAccess;
using Entities.Concrete.Products;

namespace DataAccess.Abstract.Products;

public interface IDiscountDal : IEntityRepository<Discount>
{
    // Additional methods specific to Discount can be added here
}