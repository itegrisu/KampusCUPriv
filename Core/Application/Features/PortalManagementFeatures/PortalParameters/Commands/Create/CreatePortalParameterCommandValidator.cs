using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Create;

public class CreatePortalParameterCommandValidator : AbstractValidator<CreatePortalParameterCommand>
{
    public CreatePortalParameterCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.ParameterValueType).NotNull().NotEmpty();
RuleFor(c => c.StringValue);
RuleFor(c => c.Description);


    }
}