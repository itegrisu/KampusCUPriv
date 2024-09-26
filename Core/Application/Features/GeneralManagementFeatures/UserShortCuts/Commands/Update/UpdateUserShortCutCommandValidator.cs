using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Update;

public class UpdateUserShortCutCommandValidator : AbstractValidator<UpdateUserShortCutCommand>
{
    public UpdateUserShortCutCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
        RuleFor(c => c.PageName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.PageUrl).NotNull().NotEmpty().MaximumLength(150);


    }
}