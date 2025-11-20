using EventPulseAPI.Common.Enums;
using EventPulseAPI.Common.Helpers;
using EventPulseAPI.Data.Data;
using EventPulseAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPulseAPI.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(EventPulseContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any(u => u.Email == "admin@eventpulse.com"))
            {
                PasswordHasher.CreatePasswordHash("Admin@123", out var hash, out var salt);
                var admin = new User
                {
                    FullName = "System Admin",
                    Email = "admin@eventpulse.com",
                    Role = UserRole.Admin,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };
                context.Users.Add(admin);
            }

            if (!context.Users.Any(u => u.Email == "organizer@eventpulse.com"))
            {
                PasswordHasher.CreatePasswordHash("Organizer@123", out var hash, out var salt);
                var organizer = new User
                {
                    FullName = "Event Organizer",
                    Email = "organizer@eventpulse.com",
                    Role = UserRole.Organizer,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };
                context.Users.Add(organizer);
            }

            if (!context.Users.Any(u => u.Email == "attendee@eventpulse.com"))
            {
                PasswordHasher.CreatePasswordHash("Attendee@123", out var hash, out var salt);
                var attendee = new User
                {
                    FullName = "Event Attendee",
                    Email = "attendee@eventpulse.com",
                    Role = UserRole.Attendee,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };
                context.Users.Add(attendee);
            }

            context.SaveChanges();
        }
    }
}
