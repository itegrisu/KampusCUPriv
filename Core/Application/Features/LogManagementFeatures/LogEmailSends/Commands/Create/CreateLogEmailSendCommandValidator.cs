using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Create;

public class CreateLogEmailSendCommandValidator : AbstractValidator<CreateLogEmailSendCommand>
{
    public CreateLogEmailSendCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(200);
        RuleFor(c => c.Content).NotNull().NotEmpty().MaximumLength(1000);


    }
}