using FluentValidation;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Create;

public class CreateAnnouncementTypeCommandValidator : AbstractValidator<CreateAnnouncementTypeCommand>
{
    public CreateAnnouncementTypeCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(150);


    }
}