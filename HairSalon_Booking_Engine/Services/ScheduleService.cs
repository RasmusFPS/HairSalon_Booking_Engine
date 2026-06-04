using HairSalon_Booking_Engine.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly HairSalonDBContext _ctx;

        public ScheduleService(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<GetScheduleResponse>> GetAllAsync()
        {
            return await _ctx.Schedules
                .AsNoTracking()
                .Select(c => new GetScheduleResponse(
                    c.Id,
                    c.DayOfWeek,
                    c.WorkStart,
                    c.WorkEnd,
                    c.LunchTime,
                    new GetStylistResponse(c.StylistId, c.Stylist.FirstName, c.Stylist.LastName ?? "")))
                .ToListAsync();
        }

        public async Task<List<GetScheduleResponse>> GetByIdAsync(int id)
        {
            return await _ctx.Schedules
                .AsNoTracking()
                .Where(s => s.StylistId == id)
                .Select(s => new GetScheduleResponse(
                    s.Id,
                    s.DayOfWeek,
                    s.WorkStart,
                    s.WorkEnd,
                    s.LunchTime,
                    new GetStylistResponse(
                        s.Stylist.Id,
                        s.Stylist.FirstName,
                        s.Stylist.LastName ?? ""
                    ))).ToListAsync();
        }
    }
}
