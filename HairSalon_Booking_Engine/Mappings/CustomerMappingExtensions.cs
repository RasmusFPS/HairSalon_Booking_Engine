using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Mappings
{
    public static class CustomerMappingExtensions
    {
        public static GetCustomerResponse ToGetCustomerResponse(this Customer customer)
        {
            return new GetCustomerResponse(
                customer.Id,
                customer.FirstName,
                customer.LastName,
                customer.Phone,
                customer.Email
            );
        }

        public static IEnumerable<GetCustomerResponse> ToGetCustomerResponseList(this IEnumerable<Customer> customers)
        {
            return customers.Select(c => c.ToGetCustomerResponse());
        }
    }
}
