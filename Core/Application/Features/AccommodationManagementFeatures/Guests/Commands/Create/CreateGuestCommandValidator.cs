using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Create;

public class CreateGuestCommandValidator : AbstractValidator<CreateGuestCommand>
{
    public CreateGuestCommandValidator()
    {
        //RuleFor(c => c.GidNationalityFK);//

        RuleFor(c => c.IdNumber).NotNull().NotEmpty().MaximumLength(20);
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Surename).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Duty).MaximumLength(50);
        RuleFor(c => c.Institution).MaximumLength(50);
        RuleFor(c => c.Phone).MaximumLength(20);
        RuleFor(c => c.Email).MaximumLength(100);
        RuleFor(c => c.Gender).NotNull().NotEmpty();
        RuleFor(c => c.HesCode).MaximumLength(50);
        RuleFor(c => c.Description).MaximumLength(250);
    }
}