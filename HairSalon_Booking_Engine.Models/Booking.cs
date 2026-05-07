using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime BookedDate { get; set; }
        public int StylistId { get; set; }
        public int CustomerId { get; set; }

        // navigation properties
        public Stylist Stylist { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public ICollection<Treatment> Treatments { get; set; } = null!;
    }
}
