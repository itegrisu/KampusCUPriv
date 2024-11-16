using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.TyreTypes.Commands.Delete;

public class DeleteTyreTypeCommandValidator : AbstractValidator<DeleteTyreTypeCommand>
{
    public DeleteTyreTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}