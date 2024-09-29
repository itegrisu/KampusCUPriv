using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Create;

public class CreateMeasureTypeCommandValidator : AbstractValidator<CreateMeasureTypeCommand>
{
    public CreateMeasureTypeCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}