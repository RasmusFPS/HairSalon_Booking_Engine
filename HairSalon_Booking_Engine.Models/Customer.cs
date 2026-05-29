using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        // navigation properties
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
