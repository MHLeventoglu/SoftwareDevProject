using Business.Abstract.Products;
using Core.Utilities.Results;
using Entities.Concrete.Products;

namespace Business.Concrete.Products;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }
    public IResult Add(Product entity)
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Product entity)
    {
        throw new NotImplementedException();
    }

    public IResult Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IDataResult<Product> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
