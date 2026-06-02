using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Mappings
{
    public static class StylistMappingExtensions
    {
        public static GetStylistResponse ToGetStylistResponse(this Stylist stylist)
        {
            return new GetStylistResponse(
                stylist.Id,
                stylist.FirstName,
                stylist.LastName ?? ""
            );
        }
    }
}
