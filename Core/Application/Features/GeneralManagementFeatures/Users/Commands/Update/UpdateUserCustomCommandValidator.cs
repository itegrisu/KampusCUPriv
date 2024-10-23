using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Update;

public class UpdateUserCustomCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCustomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidNationalityFK);//

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Surname).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Title).MaximumLength(60);
        RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(c => c.PasswordHash).NotNull().NotEmpty().MaximumLength(255);
        RuleFor(c => c.SifreGuncellemeToken).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.Avatar).MaximumLength(150);
        RuleFor(c => c.IsLoginStatus).NotNull().NotEmpty();
        RuleFor(c => c.IsSystemAdmin).NotNull().NotEmpty();
        RuleFor(c => c.Gsm).NotNull().NotEmpty().MaximumLength(20);
        RuleFor(c => c.Birthplace).MaximumLength(50);
        RuleFor(c => c.IdentityNo).MaximumLength(20);
        RuleFor(c => c.PassportNo).MaximumLength(20);
        RuleFor(c => c.SGKNo).MaximumLength(50);
        RuleFor(c => c.DrivingLicenseNo).MaximumLength(50);
        RuleFor(c => c.Note).MaximumLength(300);
        RuleFor(c => c.Gender).NotNull().NotEmpty();
        RuleFor(c => c.WorkType).NotNull().NotEmpty();
        RuleFor(c => c.EmailActivationStatus).NotNull().NotEmpty();
        RuleFor(c => c.SmsActivationStatus).NotNull().NotEmpty();
        RuleFor(c => c.PersonnelSpecialNote).MaximumLength(250);
        RuleFor(c => c.IsActive).NotNull().NotEmpty();
    }
}