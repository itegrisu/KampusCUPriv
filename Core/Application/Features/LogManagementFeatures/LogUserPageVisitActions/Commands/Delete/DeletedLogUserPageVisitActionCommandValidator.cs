using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Delete;

public class DeleteLogUserPageVisitActionCommandValidator : AbstractValidator<DeleteLogUserPageVisitActionCommand>
{
    public DeleteLogUserPageVisitActionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}