using Entities.Abstract;
using Entities.Concrete.Products;

namespace Entities.Concrete.Preferences;

public class Wishlist:IEntity 
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<Product>? Items { get; set; }
    public DateTime DateAdded { get; set; }
}