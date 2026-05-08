using HairSalon_Booking_Engine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
        {
            return Ok(await _ctx.Bookings
                .AsNoTracking()
                .ToListAsync());
        }

        [HttpGet("GetById/{id}", Name = "GetBookingById")]
        public async Task<ActionResult<Booking?>> GetById(int id)
        {
            var booking = await _ctx.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (booking is null)
            {
                return NotFound($"The booking with an id of {id} could not be found.");
            }
            return Ok(booking);
        }
    }
}
