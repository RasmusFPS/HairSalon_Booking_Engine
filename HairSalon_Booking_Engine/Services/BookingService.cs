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
                .Select(b => new GetBookingResponse(
                    b.Id,
                    b.CreatedAt, 
                    b.StartTime, 
                    new GetStylistResponse(
                        b.Stylist.Id,
                        b.Stylist.FirstName, 
                        b.Stylist.LastName ?? ""), 
                    new GetCustomerResponse(
                        b.Customer.Id,
                        b.Customer.FirstName, 
                        b.Customer.LastName, 
                        b.Customer.Phone, 
                        b.Customer.Email)))
                .ToListAsync();
        }

        public async Task<GetBookingResponse?> GetByIdAsync(int id)
        {
            return await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new GetBookingResponse(
                    b.Id,
                    b.CreatedAt, 
                    b.StartTime, 
                    new GetStylistResponse(
                        b.Stylist.Id,
                        b.Stylist.FirstName, 
                        b.Stylist.LastName ?? ""),
                    new GetCustomerResponse(
                        b.Customer.Id,
                        b.Customer.FirstName, 
                        b.Customer.LastName, 
                        b.Customer.Phone, 
                        b.Customer.Email)))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest request)
        {
            var newBooking = new Booking
            {
                CreatedAt = DateTime.Now,
                StartTime = request.StartTime,
                StylistId = request.StylistId,
                CustomerId = request.CustomerId
            };

            await _ctx.Bookings.AddAsync(newBooking);
            await _ctx.SaveChangesAsync();

            var booking = await GetByIdAsync(newBooking.Id);
            return ServiceResult<GetBookingResponse>.Ok(booking!);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateBookingRequest request)
        {
            var booking = await _ctx.Bookings.FindAsync(id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            booking.StartTime = request.StartTime;
            booking.StylistId = request.StylistId;
            booking.CustomerId = request.CustomerId;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var bookingToDelete = await _ctx.Bookings.FindAsync(id);

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
