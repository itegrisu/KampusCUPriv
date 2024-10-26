using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.Delete;

public class DeleteOrganizationFileCommandValidator : AbstractValidator<DeleteOrganizationFileCommand>
{
    public DeleteOrganizationFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}