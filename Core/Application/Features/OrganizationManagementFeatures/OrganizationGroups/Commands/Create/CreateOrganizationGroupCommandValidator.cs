using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Create;

public class CreateOrganizationGroupCommandValidator : AbstractValidator<CreateOrganizationGroupCommand>
{
    public CreateOrganizationGroupCommandValidator()
    {
        RuleFor(c => c.GidOrganizationFK).NotNull().NotEmpty();

        RuleFor(c => c.GroupName).NotNull().NotEmpty().MaximumLength(100);


    }
}