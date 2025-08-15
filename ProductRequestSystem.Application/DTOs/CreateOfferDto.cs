using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.DTOs
{
    public class CreateOfferDto
    {
        public int ProductRequestId { get; set; }
        public decimal Price { get; set; }
        public int EstimatedDays { get; set; }
        public string? Comments { get; set; }
    }
}
