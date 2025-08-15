using Microsoft.EntityFrameworkCore;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Enums;
using ProductRequestSystem.Domain.Interfaces;
using ProductRequestSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Infrastructure.Repositories
{
    public class ProductRequestRepository : Repository<ProductRequest>, IProductRequestRepository
    {
        public ProductRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductRequest>> GetByClientIdAsync(string clientId)
        {
            return await _dbSet
                .Include(pr => pr.Client)
                .Include(pr => pr.Offers)
                    .ThenInclude(o => o.Provider)
                .Where(pr => pr.ClientId == clientId)
                .OrderByDescending(pr => pr.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductRequest>> GetOpenRequestsAsync()
        {
            return await _dbSet
                .Include(pr => pr.Client)
                .Include(pr => pr.Offers)
                    .ThenInclude(o => o.Provider)
                .Where(pr => pr.Status == ProductRequestStatus.Open || pr.Status == ProductRequestStatus.InNegotiation)
                .OrderByDescending(pr => pr.CreatedAt)
                .ToListAsync();
        }

        public async Task<ProductRequest?> GetWithOffersAsync(int id)
        {
            return await _dbSet
                .Include(pr => pr.Client)
                .Include(pr => pr.Offers)
                    .ThenInclude(o => o.Provider)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public override async Task<ProductRequest?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(pr => pr.Client)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }
    }

}
