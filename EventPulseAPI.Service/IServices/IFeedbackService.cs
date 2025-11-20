using EventPulseAPI.Common.Dto;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;

namespace EventPulseAPI.Services.IServices
{
    public interface IFeedbackService
    {
        Task<ApiResponse> SubmitFeedbackAsync(FeedbackCreateDto dto, User currentUser);
        Task<ApiResponse> GetFeedbackSummaryAsync(int eventId, User currentUser);
    }
}
