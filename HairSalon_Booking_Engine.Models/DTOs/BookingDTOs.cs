namespace HairSalon_Booking_Engine.Models.DTOs
{    
    public record GetBookingResponse(
        int Id, 
        DateTime CreatedAt, 
        DateTime StartTime, 
        BookingStatus Status, 
        GetStylistResponse Stylist, 
        GetCustomerResponse Customer, 
        IEnumerable<GetTreatmentResponse> Treatments);
    public record CreateBookingRequest(DateTime StartTime, int StylistId, int CustomerId, List<int> TreatmentIds);
    public record UpdateBookingRequest(DateTime StartTime, int StylistId, int CustomerId, List<int>? TreatmentIds = null);

    // Kanske borde flyttas in till en egen StylistDTOs?
    // Kanske är onödigt om det bara ska finnas en DTO?
    public record GetStylistResponse(int Id, string FirstName, string LastName);
    public record GetTreatmentResponse(int Id, string Name, string? Description, decimal Price, int DurationMin);
}
