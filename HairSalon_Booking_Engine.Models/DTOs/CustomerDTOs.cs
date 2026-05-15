using System.ComponentModel.DataAnnotations;

namespace HairSalon_Booking_Engine.Models.DTOs
{
    // record = value-based data structure--> immutable
    //public int Id { get; init; } // Vill vi ha ID här eller inte?
    public record GetCustomerResponse(string FirstName, string LastName, string Phone, string Email);
    public record CreateCustomerRequest(string FirstName, string LastName, string Phone, string Email);
}
