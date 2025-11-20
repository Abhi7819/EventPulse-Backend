using EventPulseAPI.Data.Data;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventPulseAPI.Repository.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventPulseContext _context;
        public EventRepository(EventPulseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event ev)
        {
            await _context.Events.AddAsync(ev);
        }

        public async Task DeleteAsync(Event ev)
        {
            _context.Events.Remove(ev);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Event ev)
        {
            _context.Events.Update(ev);
        }
    }
}
