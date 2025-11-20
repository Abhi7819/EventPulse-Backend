using EventPulseAPI.Data.Data;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventPulseAPI.Repository.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly EventPulseContext _context;
        public FeedbackRepository(EventPulseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
        }

        public async Task DeleteAsync(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);
        }

        public async Task<IEnumerable<Feedback>> GetAllBySessionIdAsync(int sessionId)
        {
            return await _context.Feedbacks
                .Where(f => f.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<Feedback?> GetBySessionAndAttendeeAsync(int sessionId, int attendeeId)
        {
            return await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.SessionId == sessionId && f.AttendeeId == attendeeId);
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
