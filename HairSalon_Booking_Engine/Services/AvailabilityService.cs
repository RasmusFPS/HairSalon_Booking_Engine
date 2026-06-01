using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Services
{
    public class AvailabilityService
    {
        private readonly HairSalonDBContext _ctx;
        public AvailabilityService(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }
        
    }
}
