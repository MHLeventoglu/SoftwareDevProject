using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete.Preferences;

public class Address:IEntity
{
    public int Id { get; set; }
    [Required]
    public string? AddressName { get; set; }
    public int CustomerId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? AddressDetail { get; set; }
    public string? PostalCode { get; set; }
}