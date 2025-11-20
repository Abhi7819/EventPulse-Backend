using EventPulseAPI.Data.Models;

namespace EventPulseAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
