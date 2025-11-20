using EventPulseAPI.Common.Enums;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventPulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        private User GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role)!);
            return new User { Id = userId, Email = email, Role = role };
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetAllByEvent(int eventId)
        {
            var result = await _sessionService.GetAllByEventIdAsync(eventId, GetCurrentUser());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _sessionService.GetByIdAsync(id, GetCurrentUser());
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Create([FromBody] SessionCreateDto dto)
        {
            var result = await _sessionService.CreateAsync(dto, GetCurrentUser());
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Update(int id, [FromBody] SessionCreateDto dto)
        {
            var result = await _sessionService.UpdateAsync(id, dto, GetCurrentUser());
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sessionService.DeleteAsync(id, GetCurrentUser());
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
