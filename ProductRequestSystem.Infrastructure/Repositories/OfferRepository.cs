using Microsoft.EntityFrameworkCore;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Interfaces;
using ProductRequestSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Infrastructure.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Offer>> GetByProviderIdAsync(string providerId)
        {
            return await _dbSet
                .Include(o => o.Provider)
                .Include(o => o.ProductRequest)
                    .ThenInclude(pr => pr.Client)
                .Where(o => o.ProviderId == providerId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetByProductRequestIdAsync(int productRequestId)
        {
            return await _dbSet
                .Include(o => o.Provider)
                .Include(o => o.ProductRequest)
                .Where(o => o.ProductRequestId == productRequestId)
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(o => o.Provider)
                .Include(o => o.ProductRequest)
                    .ThenInclude(pr => pr.Client)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public override async Task<Offer?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(o => o.Provider)
                .Include(o => o.ProductRequest)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }

}
