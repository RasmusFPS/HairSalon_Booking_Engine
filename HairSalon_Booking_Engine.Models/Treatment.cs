using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models
{
    public class Treatment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationMin { get; set; }

        // navigation properties
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
