using Core.Entities;

namespace Entities.DTOs.UserDtos
{
    public class UserForNotificationDto : IDto
    {
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}