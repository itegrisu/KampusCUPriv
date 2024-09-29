using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.OtoBrands.Commands.Create;

public class CreateOtoBrandCommandValidator : AbstractValidator<CreateOtoBrandCommand>
{
    public CreateOtoBrandCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}