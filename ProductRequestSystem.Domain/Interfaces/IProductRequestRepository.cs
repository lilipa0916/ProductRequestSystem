using ProductRequestSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Interfaces
{
    public interface IProductRequestRepository : IRepository<ProductRequest>
    {
        Task<IEnumerable<ProductRequest>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<ProductRequest>> GetOpenRequestsAsync();
        Task<ProductRequest?> GetWithOffersAsync(int id);
    }
}
