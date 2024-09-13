using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Commands.Update;

public class UpdateDocumentTypeCommandValidator : AbstractValidator<UpdateDocumentTypeCommand>
{
    public UpdateDocumentTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.BelgeAdi).NotNull().NotEmpty().MaximumLength(100);
    }
}