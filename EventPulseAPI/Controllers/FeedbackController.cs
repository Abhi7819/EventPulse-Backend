using EventPulseAPI.Common.Enums;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        private User GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role)!);
            return new User { Id = userId, Email = email, Role = role };
        }

        [HttpPost]
        [Authorize(Roles = "Attendee")]
        public async Task<IActionResult> Submit([FromBody] FeedbackCreateDto dto)
        {
            var result = await _feedbackService.SubmitFeedbackAsync(dto, GetCurrentUser());
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("summary/event/{eventId}")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> GetSummary(int eventId)
        {
            var result = await _feedbackService.GetFeedbackSummaryAsync(eventId, GetCurrentUser());
            return Ok(result);
        }
    }
}
