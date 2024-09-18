using CinemaApiCase.Data;
using CinemaApiCase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiCase.Controllers
{
    [Route("api/[controller]")] // api/screens
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly CinemaDbContext _context;
        public ScreensController(CinemaDbContext context)
        {
            _context = context;
        }

        // Get data on cinemas and screens
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCinemas()
        {
            var cinemas = await _context.Cinemas.Include(c => c.Screens).ToListAsync();
            if (!cinemas.Any())
            {
                return NotFound("No cinemas found.");
            }

            var cinemaDetails = new List<object>(); // list to store cinema details

            foreach (var cinema in cinemas)
            {
                var screenData = new List<object>(); // list to store screens details

                foreach (var screen in cinema.Screens)
                {
                    screenData.Add(new
                    {
                        ScreenId = screen.Id,
                        ScreenName = screen.ScreenName
                    });
                }

                cinemaDetails.Add(new
                {
                    CinemaId = cinema.Id,
                    CinemaName = cinema.Name,
                    CinemaLocation = cinema.Location,
                    Screens = screenData
                });
            }

            return Ok(cinemaDetails);
        }

        // Get the data on a specific screen
        [HttpGet("{screenId}")] // api/screens/{id}
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetScreen( int screenId )
        {
            //var screen = await _context.Screens.Include(s => s.Showtimes).ThenInclude(st => st.SeatBookings).FirstOrDefaultAsync(s => s.Id == screenId);
            var screen = await _context.Screens
                .Include(s => s.Showtimes)       // Include showtimes associated with the screen
                .ThenInclude(st => st.Movie)     // Include the Movie for each showtime
                .Include(s => s.Showtimes)       // Include seat bookings associated with the showtimes
                .ThenInclude(st => st.SeatBookings)
                .FirstOrDefaultAsync(s => s.Id == screenId);

            if (screen == null) {
                return NotFound("No screen found for the provided screen ID");
            }

            var seats = await _context.Seats.Where(s => s.ScreenId == screenId).ToListAsync();
            int totalSeats = seats.Count;

            var showtimesData = new List<object>();
            foreach ( var showtime in screen.Showtimes)
            {
                var reservedSeats = showtime.SeatBookings.Count(); /// NEED TO CHECK THIS
                showtimesData.Add(new
                {
                    ShowtimeId = showtime.Id,
                    MovieTitle = showtime.Movie.Title,
                    StartTime = showtime.StartTime,
                    ReservedSeats = reservedSeats
                });
            }

            return Ok(new
            {
                ScreenName = screen.ScreenName,
                TotalSeats = totalSeats,
                Showtimes = showtimesData
            });
        }

        // Get the abailable seats for a specific showtime
        [HttpGet("{showtimeId}/seats")] // api/screens/{showtimeId}/seats
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSeats( int showtimeId)
        {
            var showtime = await _context.Showtimes.Include(st => st.SeatBookings).ThenInclude(sb => sb.Seat).FirstOrDefaultAsync(st => st.Id == showtimeId);
            if (showtime == null)
            {
                return NotFound("No showtimes found for the provided showtime ID");
            }

            var totalSeats = await _context.Seats.Where(s => s.ScreenId == showtime.ScreenId).ToListAsync();
            var bookedSeatsIds = showtime.SeatBookings.Select(sb => sb.SeatId).Distinct().ToList(); // get the seats that are booked from the showtime object

            var seatPrices = new Dictionary<SeatType, int> // Define seat prices based on SeatType
            {
                { SeatType.Standard, 75 },
                { SeatType.Delux, 150 },
                { SeatType.Handicap, 60 }
            };

            var availableSeats = totalSeats.Where(s => !bookedSeatsIds.Contains(s.Id)).Select(s => new SeatDTO
            {
                Id = s.Id,
                SeatNumber = $"{s.Row}{s.Column}",
                Row = s.Row,
                Column = s.Column,
                SeatType = s.SeatType,
                Price = seatPrices[s.SeatType]
            }).ToList();
            //var availableSeats = totalSeats.Where(s => !bookedSeatsIds.Contains(s.Id)).ToList(); // all not booked seats

            var bookedSeats = totalSeats.Where(s => bookedSeatsIds.Contains(s.Id)).Select(s => new SeatDTO
            {
                Id = s.Id,
                SeatNumber = $"{s.Row}{s.Column}",
                Row = s.Row,
                Column = s.Column,
                SeatType = s.SeatType,
                Price = seatPrices[s.SeatType]
            }).ToList();
            //var bookedSeats = totalSeats.Where(s => bookedSeatsIds.Contains(s.Id)).ToList(); // all booked seats

            return Ok(new
            {
                ShowtimeId = showtimeId,
                ScreenId = showtime.ScreenId,
                TotalSeats = availableSeats.Count() + bookedSeats.Count(),
                AmountBooked = bookedSeats.Count(),
                AvailableSeats = availableSeats,
                BookedSeats = bookedSeats
                //TotalSeats = totalSeats.Count(),
                //AvailableSeats = availableSeats.Select(s => new { s.Id, s.Row, s.Column, s.SeatType }),
                //BookedSeats = bookedSeats.Select(s => new { s.Id, s.Row, s.Column, s.SeatType })
            });
        }
    }

    // GetSeats has problems with the JSON serializer and circular references, so making a DTO. Edit: Found the error (TotalSeats was set to totalSeats object without a .Count()), but keeping it now.
    public class SeatDTO
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public char Row { get; set; }
        public int Column { get; set; }
        public SeatType SeatType { get; set; }
        public int Price { get; set; }
    }
    public class ShowtimeDTO
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime ShowtimeDate { get; set; }
        public List<SeatDTO> Seats { get; set; }
    }
}
