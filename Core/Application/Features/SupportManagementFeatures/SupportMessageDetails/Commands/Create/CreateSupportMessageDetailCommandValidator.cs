using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Create;

public class CreateSupportMessageDetailCommandValidator : AbstractValidator<CreateSupportMessageDetailCommand>
{
    public CreateSupportMessageDetailCommandValidator()
    {
         RuleFor(c => c.GidSupportFK).NotNull().NotEmpty();
        RuleFor(c => c.GidReadUserFK).NotNull().NotEmpty();

    }
}