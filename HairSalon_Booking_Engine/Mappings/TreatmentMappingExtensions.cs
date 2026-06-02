using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Mappings
{
    public static class TreatmentMappingExtensions
    {
        public static GetTreatmentResponse ToGetTreatmentResponse(this Treatment treatment)
        {
            return new GetTreatmentResponse(
                treatment.Id,
                treatment.Name,
                treatment.Description,
                treatment.Price,
                treatment.DurationMin
            );
        }

        public static IEnumerable<GetTreatmentResponse> ToGetTreatmentResponseList(this IEnumerable<Treatment> treatments)
        {
            return treatments.Select(t => t.ToGetTreatmentResponse());
        }
    }
}
