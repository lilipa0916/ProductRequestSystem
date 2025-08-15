using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Entities
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime RequiredDate { get; set; }
        public ProductRequestStatus Status { get; set; } = ProductRequestStatus.Open;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public string ClientId { get; set; } = string.Empty;

        // Navigation properties
        public User Client { get; set; } = null!;
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
