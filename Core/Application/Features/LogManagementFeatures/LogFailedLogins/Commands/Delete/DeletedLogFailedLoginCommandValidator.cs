using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Delete;

public class DeleteLogFailedLoginCommandValidator : AbstractValidator<DeleteLogFailedLoginCommand>
{
    public DeleteLogFailedLoginCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}