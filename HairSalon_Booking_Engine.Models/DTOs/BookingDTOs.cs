using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models.DTOs
{    

    // kanske lägga till ANNOTATIONS inom DTOS
    // tänk på Fluent Validations
    public record GetBookingRequest(DateTime BookingDate, DateTime BookedDate, int StylistId, int CustomerId);
    public record CreateBookingRequest(DateTime BookingDate, DateTime BookedDate, int StylistId, int CustomerId);

}
