using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Commands.Delete;

public class DeleteOrganizationItemFileCommandValidator : AbstractValidator<DeleteOrganizationItemFileCommand>
{
    public DeleteOrganizationItemFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}