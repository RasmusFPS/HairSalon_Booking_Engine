using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers.Extensions
{
    public static class ServiceResultExtensions
    {
        public static ActionResult ToActionResult(this ServiceResult result)
        {
            return result.Status switch
            {
                ServiceResultStatus.NotFound => new NotFoundObjectResult(result.ErrorMessage),
                ServiceResultStatus.ValidationError => new BadRequestObjectResult(result.ErrorMessage),
                _ => new BadRequestObjectResult(result.ErrorMessage)
            };
        }

        public static ActionResult ToActionResult<T>(this ServiceResult<T> result) where T : class
        {
            return result.Status switch
            {
                ServiceResultStatus.NotFound => new NotFoundObjectResult(result.ErrorMessage),
                ServiceResultStatus.ValidationError => new BadRequestObjectResult(result.ErrorMessage),
                _ => new BadRequestObjectResult(result.ErrorMessage)
            };
        }
    }
}