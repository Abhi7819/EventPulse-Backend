using EventPulseAPI.Common.Dto;
using EventPulseAPI.Common.Enums;
using EventPulseAPI.Common.Helpers;
using EventPulseAPI.Data.Models;
using EventPulseAPI.Dto.Dto;
using EventPulseAPI.Repository.IRepositories;
using EventPulseAPI.Services.IServices;

namespace EventPulseAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenGenerator _jwt;

        public UserService(IUserRepository userRepo, IJwtTokenGenerator jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }

        public async Task<ApiResponse> LoginAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);

            if (user == null)
                return new ApiResponse(false, "User not found", statusCode: 404);

            if (!string.Equals(email, user.Email, StringComparison.Ordinal))
                return new ApiResponse(false, "Email case mismatch. Enter email exactly as registered.", statusCode: 400);

            if (!PasswordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ApiResponse(false, "Invalid password", statusCode: 401);

            var token = _jwt.GenerateToken(user);

            return new ApiResponse(
                true,
                "Login successful",
                new { Token = token, Role = user.Role, RoleName = user.Role.ToString() },
                statusCode: 200
            );
        }

        public async Task<ApiResponse> RegisterAsync(UserRegisterDto dto)
        {
            if (!Enum.IsDefined(typeof(UserRole), dto.Role))
            {
                return new ApiResponse(false, "Invalid role. Allowed roles are: Admin, Organizer, Attendee.", statusCode: 400);
            }

            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new ApiResponse(false, "Email already exists", statusCode: 409);
            }

            PasswordHasher.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

            var newUser = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _userRepo.AddUserAsync(newUser);
            await _userRepo.SaveChangesAsync();

            return new ApiResponse(
                true,
                "User registered successfully",
                new { UserId = newUser.Id, Role = newUser.Role, RoleName = newUser.Role.ToString() },
                statusCode: 201
            );
        }
    }
}
