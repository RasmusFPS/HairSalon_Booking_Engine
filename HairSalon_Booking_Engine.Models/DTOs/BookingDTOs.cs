using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models.DTOs
{    
    public record GetBookingRequest(DateTime BookingDate, DateTime BookedDate, int StylistId, int CustomerId);
}
