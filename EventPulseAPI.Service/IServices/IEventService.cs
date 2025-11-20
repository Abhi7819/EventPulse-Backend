using EventPulseAPI.Common.Dto;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;

namespace EventPulseAPI.Services.IServices
{
    public interface IEventService
    {
        Task<ApiResponse> GetAllEventsAsync(User currentUser);
        Task<ApiResponse?> GetEventByIdAsync(int id, User currentUser);
        Task<ApiResponse> CreateEventAsync(EventCreateDto dto, User currentUser);
        Task<ApiResponse> UpdateEventAsync(int id, EventCreateDto dto, User currentUser);
        Task<ApiResponse> DeleteEventAsync(int id, User currentUser);
    }
}
