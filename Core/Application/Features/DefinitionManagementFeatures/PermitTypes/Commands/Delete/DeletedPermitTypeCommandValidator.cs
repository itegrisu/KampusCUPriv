using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Delete;

public class DeletePermitTypeCommandValidator : AbstractValidator<DeletePermitTypeCommand>
{
    public DeletePermitTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}