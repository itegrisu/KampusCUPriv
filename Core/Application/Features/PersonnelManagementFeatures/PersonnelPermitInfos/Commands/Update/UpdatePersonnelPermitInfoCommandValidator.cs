using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Update;

public class UpdatePersonnelPermitInfoCommandValidator : AbstractValidator<UpdatePersonnelPermitInfoCommand>
{
    public UpdatePersonnelPermitInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidPermitFK).NotNull().NotEmpty();

        RuleFor(c => c.PermitStartDate).NotNull().NotEmpty();
        RuleFor(c => c.PermitEndDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}