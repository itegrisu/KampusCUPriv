using FluentValidation;

namespace Application.Features.ClubFeatures.Clubs.Commands.Delete;

public class DeleteClubCommandValidator : AbstractValidator<DeleteClubCommand>
{
    public DeleteClubCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}