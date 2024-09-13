using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Delete;

public class DeleteLogSuccessedLoginCommandValidator : AbstractValidator<DeleteLogSuccessedLoginCommand>
{
    public DeleteLogSuccessedLoginCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}