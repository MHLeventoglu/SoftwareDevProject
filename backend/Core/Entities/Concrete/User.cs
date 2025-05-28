using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Core.Entities.Concrete;

public class User:IEntity
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public bool Status { get; set; } = true; // Varsayılan olarak aktif kullanıcı
    public DateTime DateAdded { get; set; }
}