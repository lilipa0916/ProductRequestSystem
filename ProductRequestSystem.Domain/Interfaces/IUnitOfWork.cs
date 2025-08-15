using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRequestRepository ProductRequests { get; }
        IOfferRepository Offers { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
