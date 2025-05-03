using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete.Products;

public class Category:IEntity
{
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; }
}