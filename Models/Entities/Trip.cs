using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Trip
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public long DestinationId { get; set; }
        public long EmployeeId { get; set; }
        public Destination Destination { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public List<Review> Reviews { get; set; } = new();
        public List<Booking> Bookings { get; set; } = new();
    }
}
