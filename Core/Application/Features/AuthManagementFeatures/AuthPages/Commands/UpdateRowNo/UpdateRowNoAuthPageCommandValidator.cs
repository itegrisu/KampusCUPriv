using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.UpdateRowNo;

public class UpdateRowNoAuthPageCommandValidator : AbstractValidator<UpdateRowNoAuthPageCommand>
{
    public UpdateRowNoAuthPageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}