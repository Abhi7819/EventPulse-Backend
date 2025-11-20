using EventPulseAPI.Common.Dto;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;

namespace EventPulseAPI.Services.IServices
{
    public interface ISessionService
    {
        Task<ApiResponse> GetAllByEventIdAsync(int eventId, User currentUser);
        Task<ApiResponse> GetByIdAsync(int id, User currentUser);
        Task<ApiResponse> CreateAsync(SessionCreateDto dto, User currentUser);
        Task<ApiResponse> UpdateAsync(int id, SessionCreateDto dto, User currentUser);
        Task<ApiResponse> DeleteAsync(int id, User currentUser);
    }
}
