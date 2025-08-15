using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Domain.Enums
{
    public enum ProductRequestStatus
    {
        Open = 1,
        InNegotiation = 2,
        Closed = 3,
        Cancelled = 4
    }
}
