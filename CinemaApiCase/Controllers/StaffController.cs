using CinemaApiCase.Attributes;
using CinemaApiCase.Data;
using CinemaApiCase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinemaApiCase.Controllers
{
    [Route("api/[controller]")] // api/staff
    [ApiController]
    [StaffOnly] // very rudamentory ensurance that only staff can use this. WARNING, NOT SAFE!!!
    public class StaffController : ControllerBase
    {
        private readonly CinemaDbContext _context;
        public StaffController(CinemaDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Gets metrics for a specific cinema screen.
        /// Requires the 'isStaff' header with value 'true'.
        /// </summary>
        /// <param name="screenId">The screen ID.</param>
        /// <param name="isStaff">Staff header (passed automatically in request).</param>
        /// <returns>Metrics for a Screen</returns>
        [HttpGet("screen/{screenId}")] // api/staff/screen/{screenId}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetScreenMetrics(int screenId, [FromHeader(Name = "isStaff")] bool isStaff = false)
        {
            // Fetch all showtimes for the given screen
            var showtimes = await _context.Showtimes.Include(st => st.Movie).Where(st => st.ScreenId == screenId).ToListAsync();
            if (!showtimes.Any())
            {
                return NotFound("No showtimes were found for the given screen ID");
            }

            var screen = await _context.Screens.Include(s => s.Cinema).FirstOrDefaultAsync(s => s.Id == screenId);
            if (screen == null) 
            {
                return NotFound("The screen Id does not seem to be attached to a cinema");
            }

            // Overall metrics
            int totalSeats = 0;
            int totalReservedSeats = 0;
            decimal totalCurrentIncome = 0;
            decimal totalPotentialIncome = 0;

            var seatPrices = new Dictionary<SeatType, int> // Define seat prices based on SeatType
            {
                { SeatType.Standard, 75 },
                { SeatType.Delux, 150 },
                { SeatType.Handicap, 60 }
            };

            // Create a list to hold showtime metrics
            var showtimeMetrics = new List<object>();
            foreach (var showtime in showtimes) // For each showtime, calculate the seat bookings, percentage occupied, current income, and total potential income
            {
                //var showtimeSeats = await _context.Seats.Include(s => s.SeatBookings).Where(s => s.ScreenId == screenId && s.SeatBookings.Any(sb => sb.ShowtimeId == showtime.Id)).ToListAsync();
                //var showtimeSeats = await _context.Seats.Include(s => s.SeatBookings).Where(s => s.ScreenId == screenId).ToListAsync();
                var showtimeAllSeats = await _context.Seats.Where(s => s.ScreenId == showtime.ScreenId).ToListAsync();
                var showtimeAllBookings = await _context.Seats.Include(s => s.SeatBookings).Where(s => s.ScreenId == screenId && s.SeatBookings.Any(sb => sb.ShowtimeId == showtime.Id)).ToListAsync();

                var showtimeTotalSeats = showtimeAllSeats.Count();
                var showtimeReservedSeats = showtimeAllBookings.Count();

                double showtimePercentOccupied = showtimeTotalSeats > 0 ? (double)showtimeReservedSeats / showtimeTotalSeats * 100 : 0;
                decimal showtimeCurrentIncome = showtimeAllBookings.Where(s => s.SeatBookings.Any(sb => sb.ShowtimeId == showtime.Id)).Sum(s => seatPrices[s.SeatType]);
                decimal showtimePotentialIncome = showtimeAllSeats.Sum(s => seatPrices[s.SeatType]);

                showtimeMetrics.Add(new
                {
                    ShowtimeId = showtime.Id,
                    MovieTitle = showtime.Movie.Title,
                    StartTime = showtime.StartTime,
                    TotalSeats = showtimeTotalSeats,
                    ReservedSeats = showtimeReservedSeats,
                    PercentOccupied = showtimePercentOccupied,
                    CurrentIncome = showtimeCurrentIncome,
                    potentialIncome = showtimePotentialIncome
                });

                totalSeats += showtimeTotalSeats;
                totalReservedSeats += showtimeReservedSeats;
                totalCurrentIncome += showtimeCurrentIncome;
                totalPotentialIncome += showtimePotentialIncome;
            }

            double totalPercentOccupied = totalReservedSeats > 0 ? (double)totalReservedSeats / totalSeats * 100 : 0;

            // Return the result as a JSON object
            return Ok(new
            {
                CinemaID = screen.Cinema.Id,
                Cinema = screen.Cinema.Name,
                Location = screen.Cinema.Location,
                TotalSeats = totalSeats,
                TotalReservedSeats = totalReservedSeats,
                TotalPercentOccupied = totalPercentOccupied,
                TotalCurrentIncome = totalCurrentIncome,
                TotalPotentialTotalIncome = totalPotentialIncome,
                Showtimes = showtimeMetrics
            });

        }
    }
}
