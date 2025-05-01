using FluentValidation;

namespace Application.Features.GeneralFeatures.Admins.Commands.Delete;

public class DeleteAdminCommandValidator : AbstractValidator<DeleteAdminCommand>
{
    public DeleteAdminCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}