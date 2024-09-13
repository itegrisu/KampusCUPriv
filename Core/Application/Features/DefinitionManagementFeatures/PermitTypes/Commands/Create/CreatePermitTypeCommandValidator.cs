using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Commands.Create;

public class CreatePermitTypeCommandValidator : AbstractValidator<CreatePermitTypeCommand>
{
    public CreatePermitTypeCommandValidator()
    {
        
RuleFor(c => c.IzinAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}