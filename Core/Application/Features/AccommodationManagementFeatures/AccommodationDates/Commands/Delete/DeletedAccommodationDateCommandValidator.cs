using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Delete;

public class DeleteAccommodationDateCommandValidator : AbstractValidator<DeleteAccommodationDateCommand>
{
    public DeleteAccommodationDateCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}