using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.UpdateRowNo;

public class UpdateRowNoAuthRolePageCommandValidator : AbstractValidator<UpdateRowNoAuthRolePageCommand>
{
    public UpdateRowNoAuthRolePageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}