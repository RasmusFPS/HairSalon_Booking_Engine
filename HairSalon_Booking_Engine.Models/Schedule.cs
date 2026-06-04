namespace HairSalon_Booking_Engine.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; } // enum inbyggd i System
        public TimeOnly WorkStart { get; set; }
        public TimeOnly WorkEnd { get; set; }
        public TimeOnly LunchTime { get; set; } // lunch är alltid en timma
        public int StylistId { get; set; }

        // navigation properties
        public Stylist Stylist { get; set; } = null!;
    }
}
