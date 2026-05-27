namespace HairSalon_Booking_Engine.Models.DTOs
{    
    public record GetBookingResponse(int Id, DateTime CreatedAt, DateTime StartTime, GetStylistResponse Stylist, GetCustomerResponse Customer);
    public record CreateBookingRequest(DateTime StartTime, int StylistId, int CustomerId);
    public record UpdateBookingRequest(DateTime StartTime, int StylistId, int CustomerId);

    // Kanske borde flyttas in till en egen StylistDTOs?
    // Kanske är onödigt om det bara ska finnas en DTO?
    public record GetStylistResponse(int Id, string FirstName, string LastName);
}
