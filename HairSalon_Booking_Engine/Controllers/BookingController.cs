using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly HairSalonDBContext _ctx;

        public BookingController(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }


    }
}
