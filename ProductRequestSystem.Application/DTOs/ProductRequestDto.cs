using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.DTOs
{
    public class ProductRequestDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime RequiredDate { get; set; }
        public ProductRequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public List<OfferDto> Offers { get; set; } = new();
    }
}
