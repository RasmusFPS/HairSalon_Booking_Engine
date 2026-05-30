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
                    b.EndTime, 
                    b.Status,
                    new GetStylistResponse(
                        b.Stylist.Id, 
                        b.Stylist.FirstName, 
                        b.Stylist.LastName ?? ""),
                    new GetCustomerResponse(
                        b.Customer.Id, 
                        b.Customer.FirstName, 
                        b.Customer.LastName, 
                        b.Customer.Phone, 
                        b.Customer.Email),
                    b.Treatments.Select(t => new GetTreatmentResponse(t.Id, t.Name, t.Description, t.Price, t.DurationMin))))
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
                    b.EndTime, 
                    b.Status,
                    new GetStylistResponse(
                        b.Stylist.Id, 
                        b.Stylist.FirstName, 
                        b.Stylist.LastName ?? ""),
                    new GetCustomerResponse(
                        b.Customer.Id, 
                        b.Customer.FirstName, 
                        b.Customer.LastName, 
                        b.Customer.Phone, 
                        b.Customer.Email),
                    b.Treatments.Select(t => new GetTreatmentResponse(t.Id, t.Name, t.Description, t.Price, t.DurationMin))))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest request)
        {
            // kolla ifall treatments finns och hämta dem
            var treatments = await _ctx.Treatments
                .Where(t => request.TreatmentIds.Contains(t.Id))
                .ToListAsync();

            if (treatments.Count != request.TreatmentIds.Count)
            {
                return ServiceResult<GetBookingResponse>.ValidationError(
                    "En eller flera behandlingar kunde inte hittas");
            }

            // räkna ut sluttiden baserat på treatment tiderna
            var totalDurationMin = treatments.Sum(t => t.DurationMin);
            var endTime = request.StartTime.AddMinutes(totalDurationMin);

            var newBooking = new Booking
            {
                CreatedAt = DateTime.Now,
                StartTime = request.StartTime,
                EndTime = endTime,
                StylistId = request.StylistId,
                CustomerId = request.CustomerId,
                Status = BookingStatus.Pending, // börja som pending, ändra senare
                Treatments = treatments
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

            if (booking.Status == BookingStatus.Completed || booking.Status == BookingStatus.Cancelled)
            {
                return ServiceResult.ValidationError("Kan inte uppdatera klarmarkerad eller avbokad bokning");
            }

            // räkna om sluttiden på bokningen om treatments har skickats in
            if (request.TreatmentIds?.Any() == true)
            {
                var treatments = await _ctx.Treatments
                    .Where(t => request.TreatmentIds.Contains(t.Id))
                    .ToListAsync();

                if (treatments.Count != request.TreatmentIds.Count)
                {
                    return ServiceResult.ValidationError("En eller flera behandlingar kunde inte hittas");
                }

                var totalDurationMin = treatments.Sum(t => t.DurationMin);
                booking.EndTime = request.StartTime.AddMinutes(totalDurationMin);
                booking.Treatments = treatments;
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
