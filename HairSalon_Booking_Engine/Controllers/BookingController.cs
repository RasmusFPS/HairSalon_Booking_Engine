using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HairSalonDBContext _ctx;

        public BookingController(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet(Name = "GetAllBookings")]
        public async Task<ActionResult<IEnumerable<GetBookingRequest>>> GetAll()
        {
            return Ok(await _ctx.Bookings
                .AsNoTracking()
                .ToListAsync());
        }

        [HttpGet("GetById/{id}", Name = "GetBookingById")]
        public async Task<ActionResult<GetBookingRequest?>> GetById(int id)
        {
            var booking = await _ctx.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (booking is null)
            {
                return NotFound($"The booking with an id of {id} could not be found.");
            }
            return Ok(booking);
        }

        [HttpPost("CreateBooking", Name = "CreateBooking")]
        public async Task<ActionResult<CreateBookingRequest>> CreateBooking(CreateBookingRequest request)
        {
            var newBooking = new Booking
            {
                BookedDate = request.BookingDate,
                BookingDate = request.BookingDate,
                StylistId = request.StylistId,
                CustomerId = request.CustomerId
            };

            _ctx.Bookings.Add(newBooking);

            await _ctx.SaveChangesAsync();

            return Ok(newBooking);
        }

        [HttpDelete("{id}", Name = "DeleteBookingById")]
        public async Task<ActionResult> DeleteByID(int id)
        {
            var IdToDelete = await _ctx.Bookings
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            if (IdToDelete == 0)
            {
                return NotFound($"No booking with this Id{id}");
            }
            return Ok(IdToDelete);
        }
    }
}
