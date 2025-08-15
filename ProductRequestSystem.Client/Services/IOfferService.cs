using ProductRequestSystem.Client.Enum;
using ProductRequestSystem.Client.Models;

namespace ProductRequestSystem.Client.Services
{
    public interface IOfferService
    {
        Task<OfferDto?> CreateAsync(CreateOfferDto dto);
        Task<IEnumerable<OfferDto>> GetMyOffersAsync();
        Task<OfferDto?> UpdateStatusAsync(int offerId, OfferStatus status); // ← USAR EL ENUM CORRECTO
        Task<OfferDto?> GetByIdAsync(int id);
    }
}
