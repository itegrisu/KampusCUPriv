using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Delete;

public class DeletePortalTextCommandValidator : AbstractValidator<DeletePortalTextCommand>
{
    public DeletePortalTextCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}