using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Create;

public class CreateOrganizationTypeCommandValidator : AbstractValidator<CreateOrganizationTypeCommand>
{
    public CreateOrganizationTypeCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(60);


    }
}