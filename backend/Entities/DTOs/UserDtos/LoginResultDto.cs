using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;

namespace Entities.DTOs.UserDtos
{
    public class LoginResultDto: IDto
    {
        public User User { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
