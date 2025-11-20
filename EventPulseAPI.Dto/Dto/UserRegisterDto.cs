using EventPulseAPI.Common.Enums;

namespace EventPulseAPI.Dto.Dto
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
