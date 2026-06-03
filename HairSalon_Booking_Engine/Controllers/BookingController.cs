using FluentValidation;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Models.DTOs.Validation;
using HairSalon_Booking_Engine.Services;
using HairSalon_Booking_Engine.Validation;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<CreateBookingRequest> _createBookingValidator;


        public BookingController(IBookingService bookingService, IValidator<CreateBookingRequest> createBookingValidator)
        {
            _bookingService = bookingService;
            _createBookingValidator = createBookingValidator;
        }

        [HttpGet(Name = "GetAllBookings")]
        public async Task<ActionResult<IEnumerable<GetBookingResponse>>> GetAll()
        {
            return Ok(await _bookingService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetBookingById")]
        public async Task<ActionResult<GetBookingResponse?>> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);

            if (booking is null)
            {
                return NotFound($"Kunde inte hitta någon bokning med ID: {id}");
            }
            return Ok(booking);
        }

        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult> CreateBooking(CreateBookingRequest request)
        {
            var validationResult = await _createBookingValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage,
                });

                return BadRequest(errors);
            }

            var result = await _bookingService.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), result.Data);
        }

        [HttpPut("{id}", Name = "UpdateBooking")]
        public async Task<ActionResult> Update(int id, CreateBookingRequest request)
        {
            var result = await _bookingService.UpdateAsync(id, request);

            if (!result.Success)
            {
                return NotFound($"Ingen bokning hittades med ID: {id}");
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteBookingById")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var result = await _bookingService.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound($"Ingen bokning hittades med ID: {id}");
            }

            return NoContent();
        }

        
    }
}
