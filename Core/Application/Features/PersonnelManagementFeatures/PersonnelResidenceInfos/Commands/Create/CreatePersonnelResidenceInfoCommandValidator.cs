using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Create;

public class CreatePersonnelResidenceInfoCommandValidator : AbstractValidator<CreatePersonnelResidenceInfoCommand>
{
    public CreatePersonnelResidenceInfoCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.SessionSerialNo).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.DateOfIssue).NotNull().NotEmpty();
        RuleFor(c => c.ValidityDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);


    }
}