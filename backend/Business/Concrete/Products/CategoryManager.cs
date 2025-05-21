using Business.Abstract.Products;
using Core.Utilities.Results;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using System.Collections.Generic;

namespace Business.Concrete.Products
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category entity)
        {
            _categoryDal.Add(entity);
            return new SuccessResult("Category added successfully.");
        }

        public IResult Delete(Category entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Category not found.");
            }
            _categoryDal.Delete(entity);
            return new SuccessResult("Category deleted successfully.");
        }

        public IResult Update(Category entity)
        {
            if (entity == null)
            {
                return new ErrorResult("Category not found.");
            }
            _categoryDal.Update(entity);
            return new SuccessResult("Category updated successfully.");
        }

        public IDataResult<List<Category>> GetAll()
        {
            var categories = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(categories);
        }

        public IDataResult<Category> GetById(int id)
        {
            var category = _categoryDal.Get(c => c.Id == id);
            if (category == null)
            {
                return new ErrorDataResult<Category>("Category not found.");
            }
            return new SuccessDataResult<Category>(category);
        }
    }
}
