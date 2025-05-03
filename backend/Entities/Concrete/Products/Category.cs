using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete.Products;

public class Category:IEntity
{
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; }
}