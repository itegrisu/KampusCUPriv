using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Delete;

public class DeletePortalParameterCommandValidator : AbstractValidator<DeletePortalParameterCommand>
{
    public DeletePortalParameterCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}