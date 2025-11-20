using EventPulseAPI.Data.Data;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventPulseAPI.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventPulseContext _context;
        public UserRepository(EventPulseContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
