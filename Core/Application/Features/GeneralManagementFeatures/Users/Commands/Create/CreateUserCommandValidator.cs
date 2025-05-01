using FluentValidation;

namespace Application.Features.GeneralFeatures.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        //RuleFor(c => c.GidDepartmentFK);//
        //RuleFor(c => c.GidClassFK);//

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(300);
        RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(50);


    }
}