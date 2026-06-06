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
            var endTime = request.StartTime.AddMinutes(treatments.Sum(t => t.DurationMin));

            if (!await IsStylistAvailableAsync(request.StylistId, request.StartTime, endTime))
            {
                return ServiceResult<GetBookingResponse>.ValidationError(
                    "Frisören är inte tillgänglig för den valda tiden");
            }

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
                return ServiceResult.ValidationError(
                    "Kan inte uppdatera klarmarkerad eller avbokad bokning");
            }

            // räkna om sluttiden på bokningen om treatments har skickats in
            if (request.TreatmentIds?.Any() == true)
            {
                var treatments = await _ctx.Treatments
                    .Where(t => request.TreatmentIds.Contains(t.Id))
                    .ToListAsync();

                if (treatments.Count != request.TreatmentIds.Count)
                {
                    return ServiceResult.ValidationError(
                        "En eller flera behandlingar kunde inte hittas");
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
                return ServiceResult.ValidationError(
                    "Bokningen måste innehålla minst en behandling");
            }

            var endTime = request.StartTime.AddMinutes(booking.Treatments.Sum(t => t.DurationMin));

            if (!await IsStylistAvailableAsync(request.StylistId, request.StartTime, endTime, excludeBookingId: id))
            {
                return ServiceResult.ValidationError(
                    "Frisören är inte tillgänglig för den valda tiden");
            }

            booking.StartTime = request.StartTime;
            booking.EndTime = endTime;
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

        public async Task<bool> IsStylistAvailableAsync(
            int stylistId, 
            DateTime startTime, 
            DateTime endTime, 
            int? excludeBookingId = null)
        {
            // kollar så att frisören har ett schema som täcker
            // den förfrågade bokningstiden helt
            bool coveredBySchedule = await _ctx.Schedules
                .AnyAsync(s =>
                    s.StylistId == stylistId &&
                    s.DayOfWeek == startTime.DayOfWeek &&
                    s.WorkStart <= TimeOnly.FromDateTime(startTime) &&
                    s.WorkEnd >= TimeOnly.FromDateTime(endTime) &&
                    (TimeOnly.FromDateTime(endTime) <= s.LunchTime ||
                    TimeOnly.FromDateTime(startTime) >= s.LunchTime.AddHours(1)));

            if (!coveredBySchedule)
            {
                return false;
            }

            // kollar så att frisören inte redan har några bokningar under den valda tiden
            var conflictingBookings = _ctx.Bookings
                .Where(b =>
                    b.StylistId == stylistId &&
                    b.Status != BookingStatus.Cancelled &&
                    b.StartTime < endTime &&
                    b.EndTime > startTime);

            // denna raden finns till så att nuvarande bokning
            // inte kan blockera sig själv när tiden uppdateras
            if (excludeBookingId.HasValue)
            {
                conflictingBookings = conflictingBookings.Where(b => b.Id != excludeBookingId.Value);
            }

            return !await conflictingBookings.AnyAsync();
        }

        public async Task<ServiceResult<GetAvailableTimesResponse>> GetAvailableTimesAsync(DateOnly date, int stylistId)
        {
            var schedule = await _ctx.Schedules
                .FirstOrDefaultAsync(s =>
                    s.StylistId == stylistId &&
                    s.DayOfWeek == date.DayOfWeek);

            if (schedule is null)
            {
                return ServiceResult<GetAvailableTimesResponse>.ValidationError(
                    "Frisören arbetar inte den valda dagen");
            }

            var startOfDay = date.ToDateTime(schedule.WorkStart);
            var endOfDay = date.ToDateTime(schedule.WorkEnd);

            var bookings = await _ctx.Bookings
                .Where(b => 
                    b.StylistId == stylistId &&
                    b.Status != BookingStatus.Cancelled &&
                    b.StartTime >= startOfDay &&
                    b.StartTime < endOfDay)
                .ToListAsync();

            var availableTimes = new List<TimeOnly>();

            var currentTime = startOfDay;

            while (currentTime.AddHours(1) <= endOfDay)
            {
                var currentTimeOnly = TimeOnly.FromDateTime(currentTime);

                bool isLunch = currentTimeOnly >= schedule.LunchTime &&
                    currentTimeOnly < schedule.LunchTime.AddHours(1);

                bool isBooked = bookings.Any(b => currentTime >= b.StartTime && currentTime < b.EndTime);

                if (!isLunch && !isBooked)
                {
                    availableTimes.Add(currentTimeOnly);
                }
                currentTime = currentTime.AddHours(1);
            }

            var response = new GetAvailableTimesResponse(date, stylistId, availableTimes);
            return ServiceResult<GetAvailableTimesResponse>.Ok(response);
        }

        public async Task<ServiceResult> ChangeStatusAsync(int id, BookingStatus status)
        {
            var booking = await _ctx.Bookings.FindAsync(id);

            if (booking is null)
            {
                return ServiceResult.NotFound($"Ingen bokning hittades med ID: {id}");
            }

            booking.Status = status;
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

            if (booking.Status is BookingStatus.Completed)
            {
                return ServiceResult.ValidationError("En genomförd bokning kan inte avbokas.");
            }

            booking.Status = BookingStatus.Cancelled;
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> RescheduleAsync(int id, RescheduleBookingRequest request)
        {
            var booking = await _ctx.Bookings
                .Include(b => b.Treatments)
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
            var newEndTime = request.NewStartTime.AddMinutes(booking.Treatments.Sum(t => t.DurationMin));

            if (!await IsStylistAvailableAsync(stylistId, request.NewStartTime, newEndTime, excludeBookingId: id))
            {
                return ServiceResult.ValidationError(
                    "Frisören är inte tillgänglig för den valda tiden");
            }

            booking.StartTime = request.NewStartTime;
            booking.EndTime = newEndTime;
            booking.StylistId = stylistId;

            await _ctx.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}