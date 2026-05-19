using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Services
{
    public class BookingService : IBookingService
    {
        private readonly HairSalonDBContext _ctx;

        public BookingService(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<GetBookingResponse>> GetAllAsync()
        {
            return await _ctx.Bookings
                .AsNoTracking()
                .Select(b => new GetBookingResponse(b.CreatedAt, b.StartTime, b.StylistId, b.CustomerId))
                .ToListAsync();
        }

        public async Task<GetBookingResponse?> GetByIdAsync(int id)
        {
            return await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new GetBookingResponse(b.CreatedAt, b.StartTime, b.StylistId, b.CustomerId))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest request)
        {
            var newBooking = new Booking
            {
                CreatedAt = request.CreatedAt,
                StartTime = request.StartTime,
                StylistId = request.StylistId,
                CustomerId = request.CustomerId
            };

            await _ctx.Bookings.AddAsync(newBooking);
            await _ctx.SaveChangesAsync();

            var booking = await GetByIdAsync(newBooking.Id);
            return ServiceResult<GetBookingResponse>.Ok(booking!);
        }

        public async Task<ServiceResult> UpdateAsync(int id, CreateBookingRequest request)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            booking.CreatedAt = request.CreatedAt;
            booking.StartTime = request.StartTime;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var bookingToDelete = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingToDelete is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            _ctx.Bookings.Remove(bookingToDelete);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
