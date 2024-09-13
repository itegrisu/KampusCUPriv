using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Create;

public class CreateLogFailedLoginCommandValidator : AbstractValidator<CreateLogFailedLoginCommand>
{
    public CreateLogFailedLoginCommandValidator()
    {
        
RuleFor(c => c.Email).NotNull().NotEmpty().MaximumLength(120);
RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(30);
RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.Description).MaximumLength(50);


    }
}