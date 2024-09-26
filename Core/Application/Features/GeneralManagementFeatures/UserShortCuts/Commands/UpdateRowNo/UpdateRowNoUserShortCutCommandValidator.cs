using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.UpdateRowNo;

public class UpdateRowNoUserShortCutCommandValidator : AbstractValidator<UpdateRowNoUserShortCutCommand>
{
    public UpdateRowNoUserShortCutCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}