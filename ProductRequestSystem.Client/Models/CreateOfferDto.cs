namespace ProductRequestSystem.Client.Models
{
    public class CreateOfferDto
    {
        public int ProductRequestId { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDays { get; set; }
        public string? Comments { get; set; }
    }
}
