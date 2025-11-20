using EventPulseAPI.Data.Models;

namespace EventPulseAPI.Repository.IRepositories
{
    public interface IFeedbackRepository
    {
        Task<Feedback?> GetByIdAsync(int id);
        Task<Feedback?> GetBySessionAndAttendeeAsync(int sessionId, int attendeeId);
        Task AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(Feedback feedback);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Feedback>> GetAllBySessionIdAsync(int sessionId);
    }
}
