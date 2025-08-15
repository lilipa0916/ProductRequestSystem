using Microsoft.AspNetCore.Identity;
using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<ProductRequest> ProductRequests { get; set; } = new List<ProductRequest>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
