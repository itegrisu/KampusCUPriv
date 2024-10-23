using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.Create;

public class CreatePersonnelPassportInfoCommandValidator : AbstractValidator<CreatePersonnelPassportInfoCommand>
{
    public CreatePersonnelPassportInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.PassportNo).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.DateOfIssue).NotNull().NotEmpty();
        RuleFor(c => c.ValidityDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);

    }
}