using FluentValidation;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Update;

public class UpdatePortalTextCommandValidator : AbstractValidator<UpdatePortalTextCommand>
{
    public UpdatePortalTextCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(255);
RuleFor(c => c.Content);
RuleFor(c => c.Description);
RuleFor(c => c.IsRichTextBox).NotNull().NotEmpty();
RuleFor(c => c.ContentRich);


    }
}