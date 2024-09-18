using System.ComponentModel.DataAnnotations;

namespace CinemaApiCase.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Duration { get; set; }
        public string Genre { get; set; }
        [Required]
        public string Language { get; set; }

        public ICollection<Showtime> Showtimes { get; set; } // navigation property: A Movie has many Showtimes
    }
}
