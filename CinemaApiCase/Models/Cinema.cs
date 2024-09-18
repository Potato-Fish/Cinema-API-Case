using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CinemaApiCase.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public ICollection<Screen> Screens { get; set; } // a cinema can have multiple screens
    }
}
