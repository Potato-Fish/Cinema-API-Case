using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CinemaApiCase.Models
{
    public class Screen
    {
        public int Id { get; set; }
        [Required]
        public string ScreenName { get; set; }
        [Required]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } // a screen belongs to one cinema
        public ICollection<Showtime> Showtimes { get; set; } // A screen can have multiple showtimes

    }
}
