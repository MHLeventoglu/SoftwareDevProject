using Entities.Abstract;

namespace Entities.Concrete;

public class Wishlist:IEntity 
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<Product>? Items { get; set; }
    public DateTime DateAdded { get; set; }
}