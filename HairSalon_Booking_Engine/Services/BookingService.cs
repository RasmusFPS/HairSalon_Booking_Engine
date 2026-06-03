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

        public async Task<ServiceResult> CancelAsync(int id)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }
            
            if (booking.Status == BookingStatus.Cancelled)
            {
                return ServiceResult.ValidationError("Bokningen är redan avbokad.");
            }

            // Completed kan inte bli sann i nuvarande kod-> kan ej triggas
            //if (booking.Status is BookingStatus.Completed)
            //{
            //    return ServiceResult.ValidationError("En genomförd bokning kan inte avbokas.");
            //}

            booking.Status = BookingStatus.Cancelled;
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RescheduleAsync(int id, RescheduleBookingRequest request)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            if (booking.Status == BookingStatus.Cancelled)
            {
                return ServiceResult.ValidationError("En avbokad bokning kan inte ändras.");
            }

            if (request.NewStartTime <= DateTime.Now)
            {
                return ServiceResult.ValidationError("Den nya tiden måste ligga i framtiden.");
            }

            var stylistId = request.StylistId ?? booking.StylistId;

            bool slotTaken = await _ctx.Bookings.AnyAsync(b =>
            b.Id != booking.Id &&
            b.StylistId == stylistId &&
            b.StartTime == request.NewStartTime &&
            b.Status != BookingStatus.Cancelled);
            if (slotTaken)
            {
                return ServiceResult.ValidationError("Den nya tiden är redan bokad");
            }

            booking.StartTime = request.NewStartTime;

            if (request.StylistId.HasValue)
            {
                booking.StylistId = request.StylistId.Value;
            }
            await _ctx.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}
