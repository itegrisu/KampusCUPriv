using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Create;

public class CreateLogUserPageVisitCommandValidator : AbstractValidator<CreateLogUserPageVisitCommand>
{
    public CreateLogUserPageVisitCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

        //RuleFor(c => c.IpAddress).MaximumLength(20);
        RuleFor(c => c.PageInfo).NotNull().NotEmpty().MaximumLength(250);
        //RuleFor(c => c.SessionId).NotNull().NotEmpty().MaximumLength(100);


    }
}