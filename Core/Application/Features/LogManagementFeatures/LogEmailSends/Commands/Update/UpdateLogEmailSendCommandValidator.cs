using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogEmailSends.Commands.Update;

public class UpdateLogEmailSendCommandValidator : AbstractValidator<UpdateLogEmailSendCommand>
{
    public UpdateLogEmailSendCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(200);
RuleFor(c => c.Content).NotNull().NotEmpty().MaximumLength(1000);


    }
}