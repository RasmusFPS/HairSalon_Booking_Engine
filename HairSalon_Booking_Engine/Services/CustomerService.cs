using HairSalon_Booking_Engine.Mappings;
using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HairSalon_Booking_Engine.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HairSalonDBContext _ctx;

        public CustomerService(HairSalonDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<GetCustomerResponse>> GetAllAsync()
        {
            return (await _ctx.Customers
                .AsNoTracking()
                .ToListAsync())
                .ToGetCustomerResponseList();
        }

        public async Task<GetCustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _ctx.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return customer?.ToGetCustomerResponse();
        }

        public async Task<ServiceResult<GetCustomerResponse>> CreateAsync(CreateCustomerRequest request)
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

            var created = customer.ToGetCustomerResponse();
            return ServiceResult<GetCustomerResponse>.Ok(created);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCustomerRequest request)
        {
            var customer = await _ctx.Customers.FindAsync(id);

            if (customer is null)
            {
                return ServiceResult.NotFound($"Ingen kund hittades med ID: {id}");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Phone = request.Phone;
            customer.Email = request.Email;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var customerToDelete = await _ctx.Customers.FindAsync(id);

            if (customerToDelete is null)
            {
                return ServiceResult.NotFound($"Ingen kund hittades med ID: {id}");
            }

            _ctx.Customers.Remove(customerToDelete);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<IEnumerable<GetCustomerBookingHistoryResponse>> GetCustomerBookingHistoryByIdAsync(int customerId)
        {
            return await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.StartTime)
                .Select(b => new GetCustomerBookingHistoryResponse(
                    b.Id,
                    b.StartTime,
                    b.EndTime,
                    b.Status,
                    $"{b.Stylist.FirstName} {b.Stylist.LastName}",
                    b.Treatments.Select(t => t.Name)
                ))
                .ToListAsync();
        }
    }
}
