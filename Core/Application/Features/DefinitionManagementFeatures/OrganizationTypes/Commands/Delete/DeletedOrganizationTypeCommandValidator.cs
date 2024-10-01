using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Delete;

public class DeleteOrganizationTypeCommandValidator : AbstractValidator<DeleteOrganizationTypeCommand>
{
    public DeleteOrganizationTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}