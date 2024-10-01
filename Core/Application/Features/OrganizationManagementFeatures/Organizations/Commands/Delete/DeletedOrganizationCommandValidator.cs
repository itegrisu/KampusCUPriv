using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Commands.Delete;

public class DeleteOrganizationCommandValidator : AbstractValidator<DeleteOrganizationCommand>
{
    public DeleteOrganizationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}