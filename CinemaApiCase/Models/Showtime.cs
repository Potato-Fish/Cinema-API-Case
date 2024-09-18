using System.ComponentModel.DataAnnotations;

namespace CinemaApiCase.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ScreenId { get; set; }
        public DateTime StartTime { get; set; }
        public Screen Screen { get; set; } // link to the screen
        public Movie Movie { get; set; } // link to the movie (a Showtime belongs to a Movie)
        public ICollection<SeatBooking> SeatBookings { get; set; } // Navigation property to SeatBookings
    }
}
