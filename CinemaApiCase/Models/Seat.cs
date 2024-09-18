using System.ComponentModel.DataAnnotations;

namespace CinemaApiCase.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public int ScreenId { get; set; }  // The seat belongs to a specific screen
        public Screen Screen { get; set; } // Navigation property to the Screen

        [Required]
        public char Row { get; set; }  // Row of the seat
        [Required]
        public int Column { get; set; }  // Column number 

        public SeatType SeatType { get; set; }
        public ICollection<SeatBooking> SeatBookings { get; set; } // A seat can have multiple bookings over time
    }

    public enum SeatType
    {
        Standard, Delux, Handicap
    }
}