using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Delete;

public class DeleteOrganizationItemMessageCommandValidator : AbstractValidator<DeleteOrganizationItemMessageCommand>
{
    public DeleteOrganizationItemMessageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}