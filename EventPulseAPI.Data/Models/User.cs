using EventPulseAPI.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventPulseAPI.Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
