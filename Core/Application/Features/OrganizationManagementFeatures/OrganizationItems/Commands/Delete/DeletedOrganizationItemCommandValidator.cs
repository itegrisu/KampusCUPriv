using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Commands.Delete;

public class DeleteOrganizationItemCommandValidator : AbstractValidator<DeleteOrganizationItemCommand>
{
    public DeleteOrganizationItemCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}