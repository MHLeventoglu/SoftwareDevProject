using Business.Abstract.Products;
using Core.Utilities.Results;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using System.Collections.Generic;

namespace Business.Concrete.Products
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity)
        {
            _brandDal.Add(entity);
            return new SuccessResult("Brand added successfully.");
        }

        public IResult Delete(Brand entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Brand not found.");
            }
            _brandDal.Delete(entity);
            return new SuccessResult("Brand deleted successfully.");
        }

        public IResult Update(Brand entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Brand not found.");
            }
            _brandDal.Update(entity);
            return new SuccessResult("Brand updated successfully.");
        }

        public IDataResult<List<Brand>> GetAll()
        {
            var brands = _brandDal.GetAll();
            return new SuccessDataResult<List<Brand>>(brands);
        }

        public IDataResult<Brand> GetById(int id)
        {
            var brand = _brandDal.Get(b => b.Id == id);
            if (brand == null)
            {
                return new ErrorDataResult<Brand>("Brand not found.");
            }
            return new SuccessDataResult<Brand>(brand);
        }
    }
}
