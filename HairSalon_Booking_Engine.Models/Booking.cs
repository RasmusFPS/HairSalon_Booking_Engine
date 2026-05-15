using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        public int StylistId { get; set; }
        public int CustomerId { get; set; }

        // navigation properties
        public Stylist Stylist { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public ICollection<Treatment> Treatments { get; set; } = null!;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled,
        NoShow
    }

    
}
