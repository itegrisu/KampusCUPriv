using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;

public class CreateLogSuccessedLoginCommandValidator : AbstractValidator<CreateLogSuccessedLoginCommand>
{
    public CreateLogSuccessedLoginCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.SessionId).NotNull().NotEmpty().MaximumLength(100);


    }
}