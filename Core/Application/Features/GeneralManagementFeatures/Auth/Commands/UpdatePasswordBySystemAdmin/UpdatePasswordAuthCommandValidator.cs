using Application.Features.GeneralManagementFeatures.Auth.Commands.Login;
using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.UpdatePasswordBySystemAdmin
{
    public class UpdatePasswordAuthBySystemAdminCommandValidator: AbstractValidator<UpdatePasswordAuthBySystemAdminCommand>
    {
        public UpdatePasswordAuthBySystemAdminCommandValidator() {
            RuleFor(c => c.Gid).NotEmpty().NotNull();
            RuleFor(c => c.GidUser).NotEmpty().NotNull();
            RuleFor(c => c.NewPassword).NotEmpty().NotNull().Length(8, 12).WithMessage("Password must be between 8 and 20 characters.");


        }
    }
}
