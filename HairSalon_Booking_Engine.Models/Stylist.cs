using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models
{
    public class Stylist
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        // navigation properties
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
