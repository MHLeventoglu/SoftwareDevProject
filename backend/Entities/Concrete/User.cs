using Entities.Abstract;

namespace Entities.Concrete;

public class User:IEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateAdded { get; set; }
}