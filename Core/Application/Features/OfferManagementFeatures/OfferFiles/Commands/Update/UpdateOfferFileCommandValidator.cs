using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Update;

public class UpdateOfferFileCommandValidator : AbstractValidator<UpdateOfferFileCommand>
{
    public UpdateOfferFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidOfferFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(150);


    }
}