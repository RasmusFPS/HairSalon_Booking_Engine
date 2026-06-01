using FluentValidation;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Validation
{
    /// <summary>
    /// Validator for updating an existing customer
    /// </summary>
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(50).WithMessage("First Name is too long")
                .MinimumLength(2).WithMessage("First Name is too short")
                .Matches(@"^[\p{L}\s\-']+$").WithMessage("First Name cannot contain numbers or symbols");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(50).WithMessage("Last Name is too long")
                .MinimumLength(2).WithMessage("Last Name is too short")
                .Matches(@"^[\p{L}\s\-']+$").WithMessage("Last Name cannot contain numbers or symbols");

            // accepterar vanliga telefonnummer
            // men specialtecken (förutom + i början) måste tas bort för att det ska fungera korrekt
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]\d{6,14}$"); // E.164 standard format

            RuleFor(c => c.Email)
                .MaximumLength(254) // längsta möjliga e-post adressen enligt IETF
                .WithMessage("Email address cannot exceed 254 characters")
                .MinimumLength(6).WithMessage("Email address cannot be less than 6 characters")
                .EmailAddress() // kollar bara att @ finns med i strängen
                .WithMessage("Email address is not valid");
        }
    }
}
