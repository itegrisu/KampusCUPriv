using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;

public class CreateDepartmentUserCommandValidator : AbstractValidator<CreateDepartmentUserCommand>
{
    public CreateDepartmentUserCommandValidator()
    {
        RuleFor(c => c.GidDepartmanFK).NotNull().NotEmpty();
RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();



    }
}