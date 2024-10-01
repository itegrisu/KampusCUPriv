using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Update;

public class UpdateOrganizationGroupCommandValidator : AbstractValidator<UpdateOrganizationGroupCommand>
{
    public UpdateOrganizationGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidOrganizationFK).NotNull().NotEmpty();

        RuleFor(c => c.GroupName).NotNull().NotEmpty().MaximumLength(100);


    }
}