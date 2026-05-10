using HairSalon_Booking_Engine.Models;
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

        //[HttpGet(Name = "GetCustomerById")]

        //public async Task<ActionResult<GetCustomerResponse>> GetCustomerById()
        //{
        //    var customer
        //}

        [HttpPost(Name = "CreateCustomer")]
        public async Task<ActionResult> Create() // add DTO here
        {
            var customer = new Customer
            {

            };

            await _ctx.Customers.AddAsync(customer);
            await _ctx.SaveChangesAsync();

            return Created();
        }

        [HttpPut(Name = "UpdateCustomer")]
        public async Task<ActionResult> Update(int id) // add DTO here
        {
            var customer = await _ctx.Customers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return BadRequest($"Ingen kund hittades med ID: {id}");
            }

            // set values based on DTO
            customer.FirstName = "tempFirstName";
            customer.LastName = "tempLastName";

            await _ctx.SaveChangesAsync();

            var result = await _ctx.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteCustomerById")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var IdToDelete = await _ctx.Customers
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            if (IdToDelete == 0)
            {
                return NotFound($"No booking with this Id: {id}");
            }
            return Ok(IdToDelete);
        }
    }
}
