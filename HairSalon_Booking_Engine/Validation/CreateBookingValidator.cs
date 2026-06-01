using FluentValidation;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Validation
{
    /// <summary>
    /// Validator for creating a new booking
    /// </summary>
    public class CreateBookingValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingValidator()
        {
            RuleFor(b => b.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .GreaterThan(DateTime.Now).WithMessage("You cannot make a booking in the past");

            RuleFor(b => b.StylistId)
                .NotEmpty().WithMessage("You have to choose a StylistId")
                .GreaterThan(0).WithMessage("Invalid Stylist ID.");

            RuleFor(b => b.CustomerId)
                .NotEmpty().WithMessage("You have to choose a CustomerId")
                .GreaterThan(0).WithMessage("Invalid Customer ID.");

            RuleFor(b => b.TreatmentIds)
                .NotEmpty().WithMessage("You must select at least one treatment")
                .Must(ids => ids.Count > 0).WithMessage("At least one treatment required");
        }
    }
}
