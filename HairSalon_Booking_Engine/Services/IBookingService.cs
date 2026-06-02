using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<GetBookingResponse>> GetAllAsync();
        Task<GetBookingResponse?> GetByIdAsync(int id);
        Task<IEnumerable<GetBookingResponse>> GetByFiltersAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            int? stylistId = null,
            int? customerId = null,
            BookingStatus? status = null,
            string? sortBy = "StartTime",
            bool descending = false
        );

        Task<ServiceResult<GetBookingResponse>> CreateAsync(CreateBookingRequest bookingRequest);
        Task<ServiceResult> UpdateAsync(int id, UpdateBookingRequest updatedBooking);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<GetAvailableTimesResponse>> GetAvailableTimesAsync(DateOnly date,int stylistId);
    }
}
