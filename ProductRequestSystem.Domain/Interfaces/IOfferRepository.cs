using ProductRequestSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Interfaces
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Task<IEnumerable<Offer>> GetByProviderIdAsync(string providerId);
        Task<IEnumerable<Offer>> GetByProductRequestIdAsync(int productRequestId);
        Task<Offer?> GetByIdWithDetailsAsync(int id);
    }
}
