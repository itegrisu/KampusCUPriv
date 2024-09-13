using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Create;

public class CreateDocumentTypeCommandValidator : AbstractValidator<CreateDocumentTypeCommand>
{
    public CreateDocumentTypeCommandValidator()
    {

        RuleFor(c => c.BelgeAdi).NotNull().NotEmpty().MaximumLength(100);


    }
}