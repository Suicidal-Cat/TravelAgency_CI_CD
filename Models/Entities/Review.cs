using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public long UserId  { get; set; }
        public long TripId { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        public string? Comment { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public User User { get; set; } = null!;
        public Trip Trip { get; set; } = null!;

    }
}
