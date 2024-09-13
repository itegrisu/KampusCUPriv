using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Commands.Update;

public class UpdatePortalParameterCommandValidator : AbstractValidator<UpdatePortalParameterCommand>
{
    public UpdatePortalParameterCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.ParameterValueType).NotNull().NotEmpty();
RuleFor(c => c.StringValue);
RuleFor(c => c.Description);


    }
}