using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDays { get; set; }
        public string? Comments { get; set; }
        public OfferStatus Status { get; set; } = OfferStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        public int ProductRequestId { get; set; }
        public string ProviderId { get; set; } = string.Empty;

        // Navigation properties
        public ProductRequest ProductRequest { get; set; } = null!;
        public User Provider { get; set; } = null!;
    }
}
