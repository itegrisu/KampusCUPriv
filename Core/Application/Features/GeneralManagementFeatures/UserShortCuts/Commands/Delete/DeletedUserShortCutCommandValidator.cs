using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Delete;

public class DeleteUserShortCutCommandValidator : AbstractValidator<DeleteUserShortCutCommand>
{
    public DeleteUserShortCutCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}