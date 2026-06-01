using FluentValidation;
using HairSalon_Booking_Engine.Models.DTOs;

namespace HairSalon_Booking_Engine.Validation
{
    /// <summary>
    /// Validator for updating an existing booking
    /// </summary>
    public class UpdateBookingValidator : AbstractValidator<UpdateBookingRequest>
    {
        public UpdateBookingValidator()
        {
            RuleFor(b => b.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .GreaterThan(DateTime.Now).WithMessage("You cannot reschedule to the past");

            RuleFor(b => b.StylistId)
                .GreaterThan(0).WithMessage("You have to choose a StylistId");

            RuleFor(b => b.CustomerId)
                .GreaterThan(0).WithMessage("You have to choose a CustomerId");

            // when treatment ids are null keeps existing treatments
            RuleFor(b => b.TreatmentIds)
                .Must(ids => ids == null || ids.Count > 0)
                .WithMessage("If changing treatments, at least one must be selected");
        }
    }
}
