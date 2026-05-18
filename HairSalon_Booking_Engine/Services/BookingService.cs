using Azure.Core;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
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

        public async Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest bookingRequest)
        {
            var newBooking = new Booking
            {
                CreatedAt = bookingRequest.CreatedAt,
                StartTime = bookingRequest.StartTime,
                StylistId = bookingRequest.StylistId,
                CustomerId = bookingRequest.CustomerId
            };

            await _ctx.Bookings.AddAsync(newBooking);
            await _ctx.SaveChangesAsync();

            var booking = await GetByIdAsync(newBooking.Id);
            return ServiceResult<GetBookingResponse>.Ok(booking!);
        }


        public async Task<ServiceResult> UpdateAsync(int id, CreateBookingRequest updatedBooking)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"No Customer with this ID: {id}");
            }

            booking.CreatedAt = updatedBooking.CreatedAt;
            booking.StartTime = updatedBooking.StartTime;

            await _ctx.SaveChangesAsync();

            // result behöver ändras när vi kommer på hur vi ska hantera FK
            var result = await _ctx.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var IdToDelete = await _ctx.Bookings
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            if (IdToDelete == 0)
            {
                return ServiceResult.NotFound($"No booking with this Id{id}");
            }
            return ServiceResult.Ok();
        }
    }
}
