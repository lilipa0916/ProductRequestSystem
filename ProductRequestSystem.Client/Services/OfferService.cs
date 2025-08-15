using ProductRequestSystem.Client.Enum;
using ProductRequestSystem.Client.Models;
using System.Net.Http.Headers;

namespace ProductRequestSystem.Client.Services
{
    public class OfferService : IOfferService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public OfferService(HttpClient httpClient, IAuthService authService)
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

        public async Task<OfferDto?> CreateAsync(CreateOfferDto dto)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync("/api/offers", dto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OfferDto>();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<OfferDto>> GetMyOffersAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.GetAsync("/api/offers/my-offers");

                if (response.IsSuccessStatusCode)
                {
                    var offers = await response.Content.ReadFromJsonAsync<IEnumerable<OfferDto>>();
                    return offers ?? new List<OfferDto>();
                }

                return new List<OfferDto>();
            }
            catch
            {
                return new List<OfferDto>();
            }
        }

        // ← MÉTODO CORREGIDO
        public async Task<OfferDto?> UpdateStatusAsync(int offerId, OfferStatus status)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync($"/api/offers/{offerId}/status", status);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OfferDto>();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<OfferDto?> GetByIdAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _httpClient.GetAsync($"/api/offers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OfferDto>();
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
