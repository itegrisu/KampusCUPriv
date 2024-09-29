using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.Register
{
    public class RegisterAuthCustomCommandValidator : AbstractValidator<RegisterAuthCommand>
    {
        public RegisterAuthCustomCommandValidator()
        {
            // RuleFor(c => c.GidAcademicTitleFK).NotEmpty();
            // RuleFor(c => c.GidInstituteFK).NotEmpty();
            RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.Surname).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.Gsm).NotNull().NotEmpty().MaximumLength(20);

        }
    }
}