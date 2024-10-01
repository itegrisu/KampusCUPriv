using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Commands.Delete;

public class DeleteOrganizationGroupCommandValidator : AbstractValidator<DeleteOrganizationGroupCommand>
{
    public DeleteOrganizationGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}