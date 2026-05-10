namespace HairSalon_Booking_Engine.Models.DTOs
{
    // record = value-based data structure--> immutable
    public record GetCustomerResponse
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        public string Phone { get; init; } = string.Empty;
        public string? Email { get; init; }
    }
}
