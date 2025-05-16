using Core.Entities;

namespace Entities.DTOs.UserDtos;

public class UserForRegisterDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
}
