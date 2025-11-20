using EventPulseAPI.Common.Dto;
using EventPulseAPI.Common.Enums;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Repository.IRepositories;
using EventPulseAPI.Services.IServices;

namespace EventPulseAPI.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        private readonly IEventRepository _eventRepo;
        public SessionService(ISessionRepository repo, IEventRepository eventRepo)
        {
            _repo = repo;
            _eventRepo = eventRepo;
        }

        public async Task<ApiResponse> CreateAsync(SessionCreateDto dto, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin && currentUser.Role != UserRole.Organizer)
                return new ApiResponse(false, "Unauthorized");

            var ev = await _eventRepo.GetByIdAsync(dto.EventId);
            if (ev == null) return new ApiResponse(false, "Event not found");

            if (currentUser.Role == UserRole.Organizer && ev.OwnerId != currentUser.Id)
                return new ApiResponse(false, "Unauthorized");

            var session = new Session
            {
                Title = dto.Title,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                EventId = dto.EventId
            };

            await _repo.AddAsync(session);
            await _repo.SaveChangesAsync();
            return new ApiResponse(true, "Session created", session);
        }

        public async Task<ApiResponse> DeleteAsync(int id, User currentUser)
        {
            var session = await _repo.GetByIdAsync(id);
            if (session == null) return new ApiResponse(false, "Session not found");

            var ev = await _eventRepo.GetByIdAsync(session.EventId);
            if (currentUser.Role != UserRole.Admin && ev.OwnerId != currentUser.Id)
                return new ApiResponse(false, "Unauthorized");

            await _repo.DeleteAsync(session);
            await _repo.SaveChangesAsync();

            return new ApiResponse(true, "Session deleted");
        }

        public async Task<ApiResponse> GetAllByEventIdAsync(int eventId, User currentUser)
        {
            var sessions = await _repo.GetAllByEventIdAsync(eventId);
            return new ApiResponse(true, "Sessions retrieved", sessions);
        }

        public async Task<ApiResponse> GetByIdAsync(int id, User currentUser)
        {
            var session = await _repo.GetByIdAsync(id);
            if (session == null) return new ApiResponse(false, "Session not found");
            return new ApiResponse(true, "Session retrieved", session);
        }

        public async Task<ApiResponse> UpdateAsync(int id, SessionCreateDto dto, User currentUser)
        {
            var session = await _repo.GetByIdAsync(id);
            if (session == null) return new ApiResponse(false, "Session not found");

            var ev = await _eventRepo.GetByIdAsync(session.EventId);
            if (currentUser.Role != UserRole.Admin && ev.OwnerId != currentUser.Id)
                return new ApiResponse(false, "Unauthorized");

            session.Title = dto.Title;
            session.StartTime = dto.StartTime;
            session.EndTime = dto.EndTime;

            await _repo.UpdateAsync(session);
            await _repo.SaveChangesAsync();

            return new ApiResponse(true, "Session updated", session);
        }
    }
}
