using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly HairSalonDBContext _ctx;

        public CustomerController(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }
    }
}
