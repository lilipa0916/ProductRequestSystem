namespace ProductRequestSystem.Client.Models
{
    public class CreateProductRequestDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime RequiredDate { get; set; } = DateTime.Today.AddDays(1);
    }
}
