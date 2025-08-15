using ProductRequestSystem.Domain.Enums;

namespace ProductRequestSystem.Application.DTOs
{
    public class OfferDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDays { get; set; }
        public string? Comments { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductRequestId { get; set; }
        public string ProviderId { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public ProductRequestDto? ProductRequest { get; set; }
    }
}
