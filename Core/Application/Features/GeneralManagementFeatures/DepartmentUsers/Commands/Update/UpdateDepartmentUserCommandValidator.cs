using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Update;

public class UpdateDepartmentUserCommandValidator : AbstractValidator<UpdateDepartmentUserCommand>
{
    public UpdateDepartmentUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidDepartmanFK).NotNull().NotEmpty();
RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();



    }
}