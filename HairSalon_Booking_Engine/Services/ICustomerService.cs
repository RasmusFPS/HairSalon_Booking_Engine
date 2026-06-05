using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetCustomerResponse>> GetAllAsync();
        Task<GetCustomerResponse?> GetByIdAsync(int id);
        Task<ServiceResult<GetCustomerResponse>> CreateAsync(CreateCustomerRequest customerRequest);
        Task<ServiceResult> UpdateAsync(int id, UpdateCustomerRequest updatedCustomer);
        Task<ServiceResult> DeleteAsync(int id);
        Task<IEnumerable<GetCustomerBookingHistoryResponse>> GetCustomerBookingHistoryByIdAsync(int customerId);

    }
}
