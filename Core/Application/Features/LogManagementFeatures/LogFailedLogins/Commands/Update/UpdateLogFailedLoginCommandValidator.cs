using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Update;

public class UpdateLogFailedLoginCommandValidator : AbstractValidator<UpdateLogFailedLoginCommand>
{
    public UpdateLogFailedLoginCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(120);
RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(30);
RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.Description).MaximumLength(50);


    }
}