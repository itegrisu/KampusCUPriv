using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Create;

public class CreateGuestAccommodationPersonCommandValidator : AbstractValidator<CreateGuestAccommodationPersonCommand>
{
    public CreateGuestAccommodationPersonCommandValidator()
    {
        RuleFor(c => c.GidGuestAccommodationFK).NotNull().NotEmpty();
RuleFor(c => c.GidNationalityFK).NotNull().NotEmpty();

RuleFor(c => c.IdNumber).MaximumLength(20);
RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Description).MaximumLength(250);


    }
}