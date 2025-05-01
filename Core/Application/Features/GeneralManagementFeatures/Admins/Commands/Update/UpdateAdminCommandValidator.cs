using FluentValidation;

namespace Application.Features.GeneralFeatures.Admins.Commands.Update;

public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(50);


    }
}