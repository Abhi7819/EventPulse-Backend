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
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        private User GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role)!);
            return new User { Id = userId, Email = email, Role = role };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _eventService.GetAllEventsAsync(GetCurrentUser());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _eventService.GetEventByIdAsync(id, GetCurrentUser());
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
        {
            var response = await _eventService.CreateEventAsync(dto, GetCurrentUser());
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Update(int id, [FromBody] EventCreateDto dto)
        {
            var response = await _eventService.UpdateEventAsync(id, dto, GetCurrentUser());
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _eventService.DeleteEventAsync(id, GetCurrentUser());
            return Ok(response);
        }
    }
}
