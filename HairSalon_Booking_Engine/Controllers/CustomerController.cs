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
                .Select(c => new GetCustomerResponse(c.FirstName, c.LastName, c.Phone, c.Email))
                .ToListAsync());
        }

        [HttpGet("{id}", Name = "GetCustomerById")]

        public async Task<ActionResult<GetCustomerResponse>> GetCustomerById(int id)
        {
            var customer = await _ctx.Customers
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new GetCustomerResponse(c.FirstName, c.LastName, c.Phone, c.Email))
                .FirstOrDefaultAsync();

            if (customer is null)
            {
                return NotFound($"The customer with an id of {id} could not be found.");
            }
            return Ok(customer);
        }
        

        [HttpPost(Name = "CreateCustomer")]
        public async Task<ActionResult> Create(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            await _ctx.Customers.AddAsync(customer);
            await _ctx.SaveChangesAsync();

            return Created(); // change to CreatedAtAction when GetCustomerById is added
        }

        [HttpPut(Name = "UpdateCustomer")]
        public async Task<ActionResult> Update(int id, CreateCustomerRequest request)
        {
            var customer = await _ctx.Customers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return BadRequest($"Ingen kund hittades med ID: {id}");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Phone = request.Phone;
            customer.Email = request.Email;

            await _ctx.SaveChangesAsync();

            var result = await _ctx.Customers
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new GetCustomerResponse(c.FirstName, c.LastName, c.Phone, c.Email))
                .FirstOrDefaultAsync();

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
