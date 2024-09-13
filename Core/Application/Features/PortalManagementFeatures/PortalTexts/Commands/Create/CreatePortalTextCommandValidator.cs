using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Create;

public class CreatePortalTextCommandValidator : AbstractValidator<CreatePortalTextCommand>
{
    public CreatePortalTextCommandValidator()
    {
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(255);
RuleFor(c => c.Content);
RuleFor(c => c.Description);
RuleFor(c => c.IsRichTextBox).NotNull().NotEmpty();
RuleFor(c => c.ContentRich);


    }
}