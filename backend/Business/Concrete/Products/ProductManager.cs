using Business.Abstract.Products;
using Core.Utilities.Results;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using System.Collections.Generic;

namespace Business.Concrete.Products
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product entity)
        {
            _productDal.Add(entity);
            return new SuccessResult("Product added successfully.");
        }

        public IResult Delete(Product entity)
        {
            if (entity == null)
                return new ErrorResult("Product not found.");
            
            _productDal.Delete(entity);
            return new SuccessResult("Product deleted successfully.");
        }

        public IResult Update(Product entity)
        {
            if (entity == null)
                return new ErrorResult("Product not found.");
            
            _productDal.Update(entity);
            return new SuccessResult("Product updated successfully.");
        }

        public IDataResult<List<Product>> GetAll()
        {
            var products = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(products);
        }

        public IDataResult<Product> GetById(int id)
        {
            var product = _productDal.Get(p => p.Id == id);
            if (product == null)
                return new ErrorDataResult<Product>("Product not found.");
            
            return new SuccessDataResult<Product>(product);
        }
    }
}
