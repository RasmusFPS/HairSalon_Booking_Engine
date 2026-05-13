using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models.DTOs
{    
    public record GetBookingRequest(DateTime CreatedAt, DateTime StartTime, int StylistId, int CustomerId);
    public record CreateBookingRequest(DateTime CreatedAt, DateTime StartTime, int StylistId, int CustomerId);

}
