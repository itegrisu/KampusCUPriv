using FluentValidation;

namespace Application.Features.GeneralFeatures.Admins.Commands.Create;

public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminCommandValidator()
    {
        
RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(50);


    }
}