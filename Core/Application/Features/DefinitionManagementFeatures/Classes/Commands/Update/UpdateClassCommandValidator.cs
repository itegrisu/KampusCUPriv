using FluentValidation;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Update;

public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
{
    public UpdateClassCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}