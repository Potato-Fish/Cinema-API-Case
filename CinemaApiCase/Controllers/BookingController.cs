using CinemaApiCase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CinemaApiCase.Models;
using CinemaApiCase.Attributes;

namespace CinemaApiCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly CinemaDbContext _context;
        public BookingController(CinemaDbContext context)
        {
            _context = context;
        }

        // This is in order to make a booking
        [HttpPost] // api/booking
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BookSeat([FromBody] SeatBookingRequest request)
        {
            // Check if the seat exists in the specified screen for the cinema
            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.ScreenId == request.ScreenId && s.Row == request.Row && s.Column == request.Column);
            if (seat == null)
            {
                //return StatusCode(StatusCodes.Status404NotFound, "The specified seat details do not exist for that screen, make sure they are correct.");
                return NotFound("The specified seat details do not exist for that screen, make sure they are correct.");
            }

            // if the showtime exists for the specific screen
            var showtime = await _context.Showtimes.FirstOrDefaultAsync(st => st.Id == request.ShowtimeId && st.ScreenId == request.ScreenId);
            if (showtime == null)
            {
                return NotFound("The specified showtime ID does not exist for the specified screen ID.");
            }

            // Check if the seat is already booked for this showtime
            var existingBooking = await _context.SeatBooking.FirstOrDefaultAsync(sb => sb.SeatId == seat.Id && sb.ShowtimeId == showtime.Id);
            if (existingBooking != null) 
            {
                return BadRequest("This seat is already booked.");
            }

            // Create the booking
            var booking = new SeatBooking
            {
                SeatId = seat.Id,
                ShowtimeId = showtime.Id,
                BookingTime = DateTime.Now
            };

            _context.SeatBooking.Add(booking);
            await _context.SaveChangesAsync();

            return Ok($"Seat booked successfully! Booking id is {booking.Id}");
        }


        // This is in order to cancel a booking 
        [HttpDelete("cancel")] // api/booking/cancel
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            // Find the booking by its ID
            var booking = await _context.SeatBooking.FirstOrDefaultAsync(sb => sb.Id == bookingId);
            if (booking == null)
            {
                return NotFound("No bookings found for the provided booking ID.");
            }

            // Delete the booking from the database
            _context.SeatBooking.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok("Booking was removed successfully!");
        }

        // Get a list of bookings
        [HttpGet("list")] // api/booking/list
        public async Task<IEnumerable<SeatBooking>> GetBookings()
        {
            return await _context.SeatBooking.ToListAsync();
        }
    }

    // DTO for booking request
    public class SeatBookingRequest
    {
        //public int CinemaId { get; set; }      // Dont need this but can include Cinema for additional validation
        public int ScreenId { get; set; }      // ID of the Screen where the seat is located
        public int ShowtimeId { get; set; }    // ID of the Showtime
        public char Row { get; set; }          // Row of the seat
        public int Column { get; set; }        // Column of the seat
    }
}