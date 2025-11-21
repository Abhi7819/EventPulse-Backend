using EventPulseAPI.Common.Dto;
using EventPulseAPI.Common.Enums;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Repository.IRepositories;
using EventPulseAPI.Services.IServices;

namespace EventPulseAPI.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse> CreateEventAsync(EventCreateDto dto, User currentUser)
        {
            if (currentUser.Role != UserRole.Admin && currentUser.Role != UserRole.Organizer)
                return new ApiResponse(false, "Unauthorized: Only Admin or Organizer can create events", statusCode: 403);

            var ev = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                OwnerId = currentUser.Id
            };

            await _repo.AddAsync(ev);
            await _repo.SaveChangesAsync();

            return new ApiResponse(true, "Event created successfully", ev, statusCode: 201);
        }

        public async Task<ApiResponse> DeleteEventAsync(int id, User currentUser)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null) return new ApiResponse(false, "Event not found", statusCode: 404);

            if (currentUser.Role != UserRole.Admin && ev.OwnerId != currentUser.Id)
                return new ApiResponse(false, "Unauthorized", statusCode: 403);

            await _repo.DeleteAsync(ev);
            await _repo.SaveChangesAsync();

            return new ApiResponse(true, "Event deleted successfully", statusCode: 200);
        }

        public async Task<ApiResponse> GetAllEventsAsync(User currentUser)
        {
            var events = await _repo.GetAllAsync();

            var eventDtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                OwnerId = e.OwnerId
            }).ToList();

            return new ApiResponse(true, "Events retrieved successfully", eventDtos, statusCode: 200);
        }

        public async Task<ApiResponse?> GetEventByIdAsync(int id, User currentUser)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null)
                return new ApiResponse(false, "Event not found", statusCode: 404);

            var eventDto = new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                OwnerId = e.OwnerId
            };

            return new ApiResponse(true, "Event retrieved successfully", eventDto, statusCode: 200);
        }

        public async Task<ApiResponse> UpdateEventAsync(int id, EventCreateDto dto, User currentUser)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null) return new ApiResponse(false, "Event not found", statusCode: 404);

            if (currentUser.Role != UserRole.Admin && ev.OwnerId != currentUser.Id)
                return new ApiResponse(false, "Unauthorized", statusCode: 403);

            ev.Title = dto.Title;
            ev.Description = dto.Description;

            await _repo.UpdateAsync(ev);
            await _repo.SaveChangesAsync();
            return new ApiResponse(true, "Event updated successfully", ev, statusCode: 200);
        }
    }
}
