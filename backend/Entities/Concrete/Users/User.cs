using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete;

public class User:IEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime DateAdded { get; set; }
}