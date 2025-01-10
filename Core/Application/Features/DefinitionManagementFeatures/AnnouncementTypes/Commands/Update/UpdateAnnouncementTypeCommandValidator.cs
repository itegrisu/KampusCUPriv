using FluentValidation;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Update;

public class UpdateAnnouncementTypeCommandValidator : AbstractValidator<UpdateAnnouncementTypeCommand>
{
    public UpdateAnnouncementTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(150);


    }
}