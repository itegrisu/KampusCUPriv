using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Commands.Update;

public class UpdateMeasureTypeCommandValidator : AbstractValidator<UpdateMeasureTypeCommand>
{
    public UpdateMeasureTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.OlcuAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}