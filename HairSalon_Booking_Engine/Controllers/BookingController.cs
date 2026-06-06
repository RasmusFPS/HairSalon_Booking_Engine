using FluentValidation;
using HairSalon_Booking_Engine.Controllers.Extensions;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<CreateBookingRequest> _createBookingValidator;
        private readonly IValidator<UpdateBookingRequest> _updateBookingValidator;

        public BookingController(
            IBookingService bookingService, 
            IValidator<CreateBookingRequest> createBookingValidator, 
            IValidator<UpdateBookingRequest> updateBookingValidator)
        {
            _bookingService = bookingService;
            _createBookingValidator = createBookingValidator;
            _updateBookingValidator = updateBookingValidator;
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

        [HttpGet("search", Name = "GetBookingsByFilters")]
        public async Task<ActionResult<IEnumerable<GetBookingResponse>>> GetByFilters(
            DateTime? dateFrom,
            DateTime? dateTo,
            int? stylistId,
            int? customerId,
            BookingStatus? status,
            string? sortBy = "StartTime",
            bool descending = false)
        {
            var bookings = await _bookingService.GetByFiltersAsync(
                dateFrom, dateTo, stylistId, customerId, status, sortBy, descending);

            return Ok(bookings);
        }

        [HttpGet("week", Name = "GetWeeklyBookings")]
        public async Task<ActionResult<IEnumerable<GetBookingResponse>>> GetWeeklyBookings(DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(7);
            var bookings = await _bookingService.GetByFiltersAsync(
                dateFrom: weekStart,
                dateTo: weekEnd,
                sortBy: "StartTime");

            return Ok(bookings);
        }

        [HttpGet("month", Name = "GetMonthlyBookings")]
        public async Task<ActionResult<IEnumerable<GetBookingResponse>>> GetMonthlyBookings(
            int year,
            int month)
        {
            var firstDay = new DateTime(year, month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            var bookings = await _bookingService.GetByFiltersAsync(
                dateFrom: firstDay,
                dateTo: lastDay,
                sortBy: "StartTime");

            return Ok(bookings);
        }

        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult> Create(CreateBookingRequest request)
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
            if (!result.Success)
            {
                return result.ToActionResult();
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}", Name = "UpdateBooking")]
        public async Task<ActionResult> Update(int id, UpdateBookingRequest request)
        {
            var validationResult = await _updateBookingValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage,
                });

                return BadRequest(errors);
            }

            var result = await _bookingService.UpdateAsync(id, request);

            if (!result.Success)
            {
                return result.ToActionResult();
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteBookingById")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var result = await _bookingService.DeleteAsync(id);

            if (!result.Success)
            {
                return result.ToActionResult();
            }

            return NoContent();
        }

        [HttpPatch("{id}/change-status", Name = "ChangeBookingStatus")]
        public async Task<ActionResult> ChangeStatus(int id, BookingStatus status)
        {
            var result = await _bookingService.ChangeStatusAsync(id, status);

            if (!result.Success)
            {
                return result.ToActionResult();
            }
            return NoContent();
        }

        [HttpPatch("{id}/cancel", Name = "CancelBooking")]
        public async Task<ActionResult> Cancel(int id)
        {
            var result = await _bookingService.CancelAsync(id);

            if (!result.Success)
            {
                return result.ToActionResult();
            }
            return NoContent();
        }

        [HttpPatch("{id}/reschedule", Name = "RescheduleBooking")]
        public async Task<ActionResult> Reschedule(int id, RescheduleBookingRequest request)
        {
            var result = await _bookingService.RescheduleAsync(id, request);

            if (!result.Success)
            {
                return result.ToActionResult();
            }
            return NoContent();
        }

        [HttpGet("AvailableTimes", Name = "GetAvailableTimesByStylistId")]
        public async Task<ActionResult> GetAvailableTimes(DateOnly date, int stylistId)
        {
            if (stylistId <= 0)
                return BadRequest("Invalid Stylist ID.");

            var result = await _bookingService.GetAvailableTimesAsync(date, stylistId);

            if (!result.Success)
                return BadRequest();

            return Ok(result.Data);
        }
    }
}
