using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Update;

public class UpdateOrganizationTypeCommandValidator : AbstractValidator<UpdateOrganizationTypeCommand>
{
    public UpdateOrganizationTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(60);


    }
}