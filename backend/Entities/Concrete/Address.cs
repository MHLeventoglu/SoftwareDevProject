using Entities.Abstract;

namespace Entities.Concrete;

public class Address:IEntity
{
    public int Id { get; set; }
    public string? AddressName { get; set; }
    public int CustomerId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? AddressDetail { get; set; }
    public string? PostalCode { get; set; }
}