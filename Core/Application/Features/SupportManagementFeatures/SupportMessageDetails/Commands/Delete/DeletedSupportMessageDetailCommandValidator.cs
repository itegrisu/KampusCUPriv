using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Delete;

public class DeleteSupportMessageDetailCommandValidator : AbstractValidator<DeleteSupportMessageDetailCommand>
{
    public DeleteSupportMessageDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}