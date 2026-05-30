namespace HairSalon_Booking_Engine.Models.DTOs
{
    public record GetScheduleResponse(int Id, DateTime StartTime, DateTime EndTime, bool Available, string? Notes, GetStylistResponse Stylist);
    public record CreateScheduleRequest(DateTime StartTime, DateTime EndTime, bool Available, string? Notes, int StylistId);
    public record UpdateScheduleRequest(DateTime StartTime, DateTime EndTime, bool Available, string? Notes, int StylistId);
}
