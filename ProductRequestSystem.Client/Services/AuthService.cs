using Blazored.LocalStorage;
using ProductRequestSystem.Client.Models;

namespace ProductRequestSystem.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    if (authResponse != null)
                    {
                        await _localStorage.SetItemAsync("authToken", authResponse.Token);
                        await _localStorage.SetItemAsync("user", authResponse.User);
                    }
                    return authResponse;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerDto);

                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    if (authResponse != null)
                    {
                        await _localStorage.SetItemAsync("authToken", authResponse.Token);
                        await _localStorage.SetItemAsync("user", authResponse.User);
                    }
                    return authResponse;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("user");
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task<UserDto?> GetCurrentUserAsync()
        {
            return await _localStorage.GetItemAsync<UserDto>("user");
        }
    }

}
