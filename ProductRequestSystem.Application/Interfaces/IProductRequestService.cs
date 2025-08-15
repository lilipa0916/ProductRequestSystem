using ProductRequestSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Interfaces
{
    public interface IProductRequestService
    {
        Task<ProductRequestDto> CreateAsync(CreateProductRequestDto dto, string clientId);
        Task<IEnumerable<ProductRequestDto>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<ProductRequestDto>> GetOpenRequestsAsync();
        Task<ProductRequestDto?> GetByIdAsync(int id);
    }
}
