using EventPulseAPI.Common.Dto;
using EventPulseAPI.Dto.Dto;

namespace EventPulseAPI.Services.IServices
{
    public interface IUserService
    {
        Task<ApiResponse> RegisterAsync(UserRegisterDto dto);
        Task<ApiResponse> LoginAsync(string email, string password);
    }
}
