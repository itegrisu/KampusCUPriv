using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Delete;

public class DeleteMeasureTypeCommandValidator : AbstractValidator<DeleteMeasureTypeCommand>
{
    public DeleteMeasureTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}