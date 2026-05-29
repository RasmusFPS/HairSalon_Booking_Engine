using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet(Name = "GetAllSchedules")]
        public async Task<ActionResult<ICollection<GetScheduleResponse>>> GetAll()
        {
            return Ok(await _scheduleService.GetAllAsync());
        }
    }
}
