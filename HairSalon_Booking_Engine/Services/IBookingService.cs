using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<GetBookingResponse>> GetAllAsync();
        Task<GetBookingResponse?> GetByIdAsync(int id);
        Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest bookingRequest);
        Task<ServiceResult> UpdateAsync(int id, UpdateBookingRequest updatedBooking);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<TimeOnly>>> GetAvailableTimesAsync(DateOnly date,int stylistId);
    }
}
