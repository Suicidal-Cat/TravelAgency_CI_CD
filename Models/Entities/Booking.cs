using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TripId { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        public User User { get; set; } = null!;
        public Trip Trip { get; set; } = null!;
    }
}
