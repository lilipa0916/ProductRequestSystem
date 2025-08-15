using ProductRequestSystem.Client.Models;

namespace ProductRequestSystem.Client.Services
{
    public interface IProductRequestService
    {
        Task<ProductRequestDto?> CreateAsync(CreateProductRequestDto dto);
        Task<IEnumerable<ProductRequestDto>> GetMyRequestsAsync();
        Task<IEnumerable<ProductRequestDto>> GetOpenRequestsAsync();
        Task<ProductRequestDto?> GetByIdAsync(int id);
    }

}
