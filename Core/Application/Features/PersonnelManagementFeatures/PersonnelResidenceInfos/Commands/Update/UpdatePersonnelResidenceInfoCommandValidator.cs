using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.Update;

public class UpdatePersonnelResidenceInfoCommandValidator : AbstractValidator<UpdatePersonnelResidenceInfoCommand>
{
    public UpdatePersonnelResidenceInfoCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.SessionSerialNo).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.DateOfIssue).NotNull().NotEmpty();
        RuleFor(c => c.ValidityDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}