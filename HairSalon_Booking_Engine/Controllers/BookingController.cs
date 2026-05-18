using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService BookingService)
        {
            _bookingService = BookingService;
        }

        [HttpGet(Name = "GetAllBookings")]
        public async Task<ActionResult<IEnumerable<GetBookingResponse>>> GetAll()
        {
            return Ok(await _bookingService.GetAllAsync());
        }

        [HttpGet("GetById/{id}", Name = "GetBookingById")]
        public async Task<ActionResult<GetBookingResponse?>> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);

            if (booking is null)
            {
                return NotFound($"The booking with an id of {id} could not be found.");
            }
            return Ok(booking);
        }

        [HttpPost("CreateBooking", Name = "CreateBooking")]
        public async Task<ActionResult> CreateBooking(CreateBookingRequest request)
        {
            var newBooking = await _bookingService.CreateAsync(request);

            if(!newBooking.Success)
            {
                return BadRequest($"Couldnt Create New booking");
            }
            return Ok(newBooking);
        }

        [HttpPut(Name = "UpdateBooking")]
        public async Task<ActionResult> Update(int id, CreateBookingRequest request)
        {
            var result = await _bookingService.UpdateAsync(id, request);

            if (!result.Success)
            {
                return NotFound($"Ingen bokning hittades med ID: {id}");
            }

            return Ok(result);

        }

        [HttpDelete("{id}", Name = "DeleteBookingById")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var result = await _bookingService.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound($"No Booking with this ID:{id}");
            }

            return Ok($"Booking {id} has been Deleted");
        }
    }
}
