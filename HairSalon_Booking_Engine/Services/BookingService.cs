using HairSalon_Booking_Engine.Mappings;
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
            return (await _ctx.Bookings
                .AsNoTracking()
                .Include(b => b.Stylist)
                .Include(b => b.Customer)
                .Include(b => b.Treatments)
                .ToListAsync())
                .ToGetBookingResponseList();
        }

        public async Task<GetBookingResponse?> GetByIdAsync(int id)
        {
            var booking = await _ctx.Bookings
                .AsNoTracking()
                .Include(b => b.Stylist)
                .Include(b => b.Customer)
                .Include(b => b.Treatments)
                .FirstOrDefaultAsync(b => b.Id == id);

            return booking?.ToGetBookingResponse();
        }

        public async Task<IEnumerable<GetBookingResponse>> GetByFiltersAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            int? stylistId = null,
            int? customerId = null,
            BookingStatus? status = null,
            string? sortBy = "StartTime",
            bool descending = false)
        {
            var query = _ctx.Bookings
                .AsNoTracking()
                .Include(b => b.Stylist)
                .Include(b => b.Customer)
                .Include(b => b.Treatments)
                .AsQueryable();

            if (dateFrom.HasValue)
            {
                query = query.Where(b => b.StartTime.Date >= dateFrom.Value.Date);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(b => b.StartTime.Date <= dateTo.Value.Date);
            }

            if (stylistId.HasValue && stylistId > 0)
            {
                query = query.Where(b => b.StylistId == stylistId.Value);
            }

            if (customerId.HasValue && customerId > 0)
            {
                query = query.Where(b => b.CustomerId == customerId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(b => b.Status == status.Value);
            }

            query = sortBy?.ToLower() switch
            {
                "starttime" => descending
                    ? query.OrderByDescending(b => b.StartTime)
                    : query.OrderBy(b => b.StartTime),

                "createdat" => descending
                    ? query.OrderByDescending(b => b.CreatedAt)
                    : query.OrderBy(b => b.CreatedAt),

                "status" => descending
                    ? query.OrderByDescending(b => b.Status)
                    : query.OrderBy(b => b.Status),

                "endtime" => descending
                    ? query.OrderByDescending(b => b.EndTime)
                    : query.OrderBy(b => b.EndTime),

                _ => query.OrderBy(b => b.StartTime)
            };

            var bookings = await query.ToListAsync();
            return bookings.ToGetBookingResponseList();
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
            var booking = await _ctx.Bookings
                .Include(b => b.Treatments)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            if (booking.Status is BookingStatus.Completed or BookingStatus.Cancelled)
            {
                return ServiceResult.ValidationError("Kan inte uppdatera klarmarkerad eller avbokad bokning");
            }

            var hasConflict = await _ctx.Bookings
                .Where(b => b.Id != id && b.StylistId == request.StylistId && b.Status != BookingStatus.Cancelled)
                .AnyAsync(b => request.StartTime < b.EndTime && request.StartTime.AddHours(1) > b.StartTime);

            if (hasConflict)
            {
                return ServiceResult.ValidationError("Frisörsalongen är redan bokad för denna tid");
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

                // töm alla treatments och fyll på med de nya om de finns
                booking.Treatments.Clear();
                foreach (var treatment in treatments)
                {
                    booking.Treatments.Add(treatment);
                }
            }

            if (!booking.Treatments.Any())
            {
                return ServiceResult.ValidationError("Bokningen måste innehålla minst en behandling");
            }

            booking.StartTime = request.StartTime;
            booking.EndTime = request.StartTime.AddMinutes(booking.Treatments.Sum(t => t.DurationMin));
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

         public async Task<ServiceResult<GetAvailableTimesResponse>> GetAvailableTimesAsync(DateOnly date, int stylistId)
        {
            var startOfDay = date.ToDateTime(new TimeOnly(9, 0));
            var endOfDay = date.ToDateTime(new TimeOnly(17, 0));

            var schedules = await _ctx.Schedules
                .Where(s => s.StylistId == stylistId && s.Available == true)
                .Where(s => s.StartTime >= startOfDay && s.StartTime <= endOfDay)
                .ToListAsync();

            var bookings = await _ctx.Bookings
                .Where(b => b.StylistId == stylistId && b.Status != BookingStatus.Cancelled)
                .ToListAsync();

            var availableTimes = new List<TimeOnly>();

            foreach (var schedule in schedules)
            {
                var currentTime = schedule.StartTime;

                while (currentTime.AddHours(1) <= schedule.EndTime)
                {
                    bool isBooked = bookings.Any(b => currentTime >= b.StartTime && currentTime < b.EndTime);

                    if (!isBooked)
                    {
                        availableTimes.Add(TimeOnly.FromDateTime(currentTime));
                    }
                    currentTime = currentTime.AddHours(1);
                }
            }


            var response = new GetAvailableTimesResponse(date, stylistId, availableTimes);

            return ServiceResult<GetAvailableTimesResponse>.Ok(response);
        }
    }
}