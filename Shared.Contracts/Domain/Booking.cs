using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }
    }
}
