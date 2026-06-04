namespace HairSalon_Booking_Engine.Models.DTOs
{
    public record GetScheduleResponse(
        int Id, 
        DayOfWeek DayOfWeek, 
        TimeOnly WorkStart, 
        TimeOnly WorkEnd, 
        TimeOnly LunchTime, 
        GetStylistResponse Stylist);

    public record CreateScheduleRequest(DayOfWeek DayOfWeek, int StylistId);
    public record UpdateScheduleRequest(DayOfWeek DayOfWeek, int StylistId);
}
