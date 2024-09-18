using System.ComponentModel.DataAnnotations;

namespace CinemaApiCase.Models
{
    public class SeatBooking
    {
        public int Id { get; set; }

        [Required]
        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; }

        [Required]
        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }

        //[Required]
        //public string UserId { get; set; }  // Could be the user making the booking, but skipping this for now
    }
}
