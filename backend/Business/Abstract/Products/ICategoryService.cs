using Entities.Concrete.Products;

namespace Business.Abstract.Products
{
    public interface ICategoryService : IBaseService<Category>
    {
        // Category'e özel metotlar varsa buraya ekle
    }
}