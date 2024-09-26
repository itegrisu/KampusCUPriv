using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Delete;

public class DeleteSupportRequestCommandValidator : AbstractValidator<DeleteSupportRequestCommand>
{
    public DeleteSupportRequestCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}