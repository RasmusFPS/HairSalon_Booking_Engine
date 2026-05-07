using HairSalon_Booking_Engine.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet(Name = "GetCustomers")]
        public async Task<ActionResult<ICollection<GetCustomerResponse>>> GetCustomers()
        {
            return Ok(await _ctx.Customers
                .AsNoTracking()
                .Select(c => new GetCustomerResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                    Email = c.Email

                }).ToListAsync());
        }



    }
}
