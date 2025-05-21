using Business.Abstract.Products;
using Core.Utilities.Results;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using System.Collections.Generic;

namespace Business.Concrete.Products
{
    public class DiscountManager : IDiscountService
    {
        private readonly IDiscountDal _discountDal;

        public DiscountManager(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }

        public IResult Add(Discount entity)
        {
            _discountDal.Add(entity);
            return new SuccessResult("Discount added successfully.");
        }

        public IResult Delete(Discount entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Discount not found.");
            }
            _discountDal.Delete(entity);
            return new SuccessResult("Discount deleted successfully.");
        }

        public IResult Update(Discount entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Discount not found.");
            }
            _discountDal.Update(entity);
            return new SuccessResult("Discount updated successfully.");
        }

        public IDataResult<List<Discount>> GetAll()
        {
            var discounts = _discountDal.GetAll();
            return new SuccessDataResult<List<Discount>>(discounts);
        }

        public IDataResult<Discount> GetById(int id)
        {
            var discount = _discountDal.Get(d => d.Id == id);
            if (discount == null)
            {
                return new ErrorDataResult<Discount>("Discount not found.");
            }
            return new SuccessDataResult<Discount>(discount);
        }
    }
}
