using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Delete;

public class DeleteLogEmailSendCommandValidator : AbstractValidator<DeleteLogEmailSendCommand>
{
    public DeleteLogEmailSendCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}