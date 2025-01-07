using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.ChangeRoom
{
    public class ChangeRoomAccommodationDateCommandValidator : AbstractValidator<ChangeRoomAccommodationDateCommand>
    {
        public ChangeRoomAccommodationDateCommandValidator()
        {
            RuleFor(c => c.Gid).NotNull().NotEmpty();
            RuleFor(c => c.NewRoomNoGid).NotNull().NotEmpty();
        }
    }
}
