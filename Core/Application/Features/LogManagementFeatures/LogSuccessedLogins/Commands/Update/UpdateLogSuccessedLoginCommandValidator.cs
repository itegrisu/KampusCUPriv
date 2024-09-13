using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Update;

public class UpdateLogSuccessedLoginCommandValidator : AbstractValidator<UpdateLogSuccessedLoginCommand>
{
    public UpdateLogSuccessedLoginCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.SessionId).NotNull().NotEmpty().MaximumLength(100);


    }
}