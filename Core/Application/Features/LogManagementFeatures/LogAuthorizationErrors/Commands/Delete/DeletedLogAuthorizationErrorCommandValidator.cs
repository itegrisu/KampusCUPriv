using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Delete;

public class DeleteLogAuthorizationErrorCommandValidator : AbstractValidator<DeleteLogAuthorizationErrorCommand>
{
    public DeleteLogAuthorizationErrorCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}