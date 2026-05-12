using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models.DTOs
{
    // record = value-based data structure--> immutable
    public record GetCustomerResponse
    {
        //public int Id { get; init; } // Vill vi ha ID här eller inte?
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        public string Phone { get; init; } = string.Empty;
        public string? Email { get; init; }
    }

    public record CreateCustomerRequest
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        [Phone]
        public string Phone { get; init; } = string.Empty;

        [EmailAddress]
        public string? Email { get; init; }
    }
}
