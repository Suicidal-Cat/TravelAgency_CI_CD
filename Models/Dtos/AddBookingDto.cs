using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class AddBookingDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [Required]
        public long TripId { get; set; }
        public DateTime BookingDate { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "You can book a trip with maximum of 5 people")]
        public int NumberOfPeople { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
    }
}
