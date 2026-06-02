using HairSalon_Booking_Engine.Models;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Mappings
{
    public static class BookingMappingExtensions
    {
        public static GetBookingResponse ToGetBookingResponse(this Booking booking)
        {
            return new GetBookingResponse(
                booking.Id,
                booking.CreatedAt,
                booking.StartTime,
                booking.EndTime,
                booking.Status,
                booking.Stylist.ToGetStylistResponse(),
                booking.Customer.ToGetCustomerResponse(),
                booking.Treatments.Select(t => t.ToGetTreatmentResponse())
            );
        }

        public static IEnumerable<GetBookingResponse> ToGetBookingResponseList(this IEnumerable<Booking> bookings)
        {
            return bookings.Select(b => b.ToGetBookingResponse());
        }
    }
}
