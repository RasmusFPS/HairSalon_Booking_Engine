namespace HairSalon_Booking_Engine.Models.DTOs
{    
    public record GetBookingResponse(
        int Id, 
        DateTime CreatedAt, 
        DateTime StartTime, 
        DateTime EndTime, 
        BookingStatus Status, 
        GetStylistResponse Stylist, 
        GetCustomerResponse Customer, 
        IEnumerable<GetTreatmentResponse> Treatments);

    public record CreateBookingRequest(
        DateTime StartTime, 
        int StylistId, 
        int CustomerId, 
        List<int> TreatmentIds);

    public record UpdateBookingRequest(
        DateTime StartTime, 
        int StylistId, 
        int CustomerId, 
        List<int>? TreatmentIds = null);

    public record GetAvailableTimesResponse(
        DateOnly Date,
        int StylistId,
        List<TimeOnly> AvailableTimes);

    public record RescheduleBookingRequest(DateTime NewStartTime, int? StylistId = null);
    public record CheckAvailabilityRequest(int StylistId, DateTime StartTime, DateTime EndTime);

    // Kanske borde flyttas in till en egen StylistDTOs?
    // Kanske är onödigt om det bara ska finnas en DTO?
    public record GetStylistResponse(int Id, string FirstName, string LastName);
    public record GetTreatmentResponse(int Id, string Name, string? Description, decimal Price, int DurationMin);
}
