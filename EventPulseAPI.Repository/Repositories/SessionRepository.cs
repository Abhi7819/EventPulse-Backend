using EventPulseAPI.Data.Data;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventPulseAPI.Repository.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly EventPulseContext _context;
        public SessionRepository(EventPulseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
        }

        public async Task DeleteAsync(Session session)
        {
            _context.Sessions.Remove(session);
        }

        public async Task<IEnumerable<Session>> GetAllByEventIdAsync(int eventId)
        {
            return await _context.Sessions
                .Where(s => s.EventId == eventId)
                .Include(s => s.Feedbacks)
                .ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions
                .Include(s => s.Feedbacks)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Session>> GetSessionsWithFeedbackByEventIdAsync(int eventId)
        {
            return await _context.Sessions
                .Where(s => s.EventId == eventId)
                .Include(s => s.Feedbacks)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Session session)
        {
            _context.Sessions.Update(session);
        }
    }
}
