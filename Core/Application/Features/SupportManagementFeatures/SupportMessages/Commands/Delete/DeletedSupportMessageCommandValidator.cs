using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Delete;

public class DeleteSupportMessageCommandValidator : AbstractValidator<DeleteSupportMessageCommand>
{
    public DeleteSupportMessageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}