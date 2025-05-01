using FluentValidation;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Create;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}