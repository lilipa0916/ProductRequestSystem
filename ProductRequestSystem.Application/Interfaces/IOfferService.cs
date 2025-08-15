using ProductRequestSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Interfaces
{
    public interface IOfferService
    {
        Task<OfferDto> CreateAsync(CreateOfferDto dto, string providerId);
        Task<IEnumerable<OfferDto>> GetByProviderIdAsync(string providerId);
        Task<OfferDto> UpdateStatusAsync(UpdateOfferStatusDto dto, string clientId);
        Task<OfferDto?> GetByIdAsync(int id);
    }
}
