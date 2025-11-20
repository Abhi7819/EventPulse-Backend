using EventPulseAPI.Data.Models;

namespace EventPulseAPI.Repository.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
