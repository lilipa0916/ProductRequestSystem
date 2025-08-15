using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.DTOs
{
    public class UpdateOfferStatusDto
    {
        public int OfferId { get; set; }
        public OfferStatus Status { get; set; }
    }
}
