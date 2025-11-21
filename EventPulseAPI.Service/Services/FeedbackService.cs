using EventPulseAPI.Common.Dto;
using EventPulseAPI.Common.Enums;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Repository.IRepositories;
using EventPulseAPI.Services.IServices;

namespace EventPulseAPI.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;
        private readonly ISessionRepository _sessionRepo;
        private readonly IEventRepository _eventRepo;

        public FeedbackService(IFeedbackRepository repo, ISessionRepository sessionRepo, IEventRepository eventRepo)
        {
            _repo = repo;
            _sessionRepo = sessionRepo;
            _eventRepo = eventRepo;
        }

        public async Task<ApiResponse> GetFeedbackSummaryAsync(int eventId, User currentUser)
        {
            var ev = await _eventRepo.GetByIdAsync(eventId);
            if (ev == null)
                return new ApiResponse(false, "Event not found", statusCode: 404);

            var sessions = await _sessionRepo.GetSessionsWithFeedbackByEventIdAsync(eventId);

            var summary = new
            {
                EventId = ev.Id,
                EventName = ev.Title,

                Sessions = sessions.Select(s => new
                {
                    SessionId = s.Id,
                    s.Title,

                    AverageRating = s.Feedbacks.Any()
                        ? Math.Round(s.Feedbacks.Average(f => f.Rating), 2)
                        : 0,

                    TotalFeedbacks = s.Feedbacks.Count,

                    FeedbackComments = s.Feedbacks.Select(f => new
                    {
                        f.Rating,
                        f.Comment
                    })
                })
            };

            return new ApiResponse(true, "Feedback summary", summary);
        }

        public async Task<ApiResponse> SubmitFeedbackAsync(FeedbackCreateDto dto, User currentUser)
        {
            if (currentUser.Role != UserRole.Attendee)
                return new ApiResponse(false, "Only attendees can submit feedback", statusCode: 403);

            var session = await _sessionRepo.GetByIdAsync(dto.SessionId);
            if (session == null) return new ApiResponse(false, "Session not found", statusCode: 404);

            var existing = await _repo.GetBySessionAndAttendeeAsync(dto.SessionId, currentUser.Id);
            if (existing != null) return new ApiResponse(false, "Feedback already submitted", statusCode: 409);

            var feedback = new Feedback
            {
                SessionId = dto.SessionId,
                AttendeeId = currentUser.Id,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            await _repo.AddAsync(feedback);
            await _repo.SaveChangesAsync();

            return new ApiResponse(true, "Feedback submitted", feedback);
        }
    }
}
