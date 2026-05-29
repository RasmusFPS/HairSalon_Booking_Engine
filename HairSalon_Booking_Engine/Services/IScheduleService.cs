using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<GetScheduleResponse>> GetAllAsync();
        //Task<GetScheduleResponse?> GetByIdAsync(int id);
        //Task<ServiceResult<GetScheduleResponse>> CreateAsync(CreateScheduleRequest customerRequest);
        //Task<ServiceResult> UpdateAsync(int id, UpdateScheduleRequest updatedCustomer);
        //Task<ServiceResult> DeleteAsync(int id);
    }
}
