using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.Create;

public class CreatePersonnelPermitInfoCommandValidator : AbstractValidator<CreatePersonnelPermitInfoCommand>
{
    public CreatePersonnelPermitInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidPermitFK).NotNull().NotEmpty();

        RuleFor(c => c.PermitStartDate).NotNull().NotEmpty();
        RuleFor(c => c.PermitEndDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}