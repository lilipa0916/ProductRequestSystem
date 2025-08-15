using ProductRequestSystem.Client.Models;

namespace ProductRequestSystem.Client.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task LogoutAsync();
        Task<string?> GetTokenAsync();
        Task<UserDto?> GetCurrentUserAsync();
    }
}
