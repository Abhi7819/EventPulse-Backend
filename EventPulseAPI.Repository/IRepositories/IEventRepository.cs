using EventPulseAPI.Data.Models;

namespace EventPulseAPI.Repository.IRepositories
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(Event ev);
        Task<bool> SaveChangesAsync();
    }
}
