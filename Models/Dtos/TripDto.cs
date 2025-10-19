using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class TripDto
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DestinationDto Destination { get; set; } = null!;
        public EmployeeDto Employee { get; set; } = null!;
        public List<ReviewDto>? Reviews { get; set; }
        public List<BookingDto>? Bookings { get; set; }
    }
}
