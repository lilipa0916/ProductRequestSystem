using ProductRequestSystem.Client.Models;
using System.Net.Http.Headers;

namespace ProductRequestSystem.Client.Services
{
    public class ProductRequestService : IProductRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public ProductRequestService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<ProductRequestDto?> CreateAsync(CreateProductRequestDto dto)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync("/api/productrequests", dto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProductRequestDto>();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductRequestDto>> GetMyRequestsAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.GetAsync("/api/productrequests/my-requests");

                if (response.IsSuccessStatusCode)
                {
                    var requests = await response.Content.ReadFromJsonAsync<IEnumerable<ProductRequestDto>>();
                    return requests ?? new List<ProductRequestDto>();
                }

                return new List<ProductRequestDto>();
            }
            catch
            {
                return new List<ProductRequestDto>();
            }
        }

        public async Task<IEnumerable<ProductRequestDto>> GetOpenRequestsAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.GetAsync("/api/productrequests/open");

                if (response.IsSuccessStatusCode)
                {
                    var requests = await response.Content.ReadFromJsonAsync<IEnumerable<ProductRequestDto>>();
                    return requests ?? new List<ProductRequestDto>();
                }

                return new List<ProductRequestDto>();
            }
            catch
            {
                return new List<ProductRequestDto>();
            }
        }

        public async Task<ProductRequestDto?> GetByIdAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.GetAsync($"/api/productrequests/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProductRequestDto>();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
