using EventPulseAPI.Data.Models;

namespace EventPulseAPI.Repository.IRepositories
{
    public interface ISessionRepository
    {
        Task<Session?> GetByIdAsync(int id);
        Task<IEnumerable<Session>> GetAllByEventIdAsync(int eventId);
        Task AddAsync(Session session);
        Task UpdateAsync(Session session);
        Task DeleteAsync(Session session);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Session>> GetSessionsWithFeedbackByEventIdAsync(int eventId);
    }
}
