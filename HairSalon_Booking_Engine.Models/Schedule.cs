namespace HairSalon_Booking_Engine.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Available { get; set; }
        public string? Notes { get; set; }
        public int StylistId { get; set; }

        // navigation properties
        public Stylist Stylist { get; set; } = null!;
    }
}
