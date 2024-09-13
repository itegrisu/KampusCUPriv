using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Update;

public class UpdateLogUserPageVisitCommandValidator : AbstractValidator<UpdateLogUserPageVisitCommand>
{
    public UpdateLogUserPageVisitCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.PageInfo).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.SessionId).NotNull().NotEmpty().MaximumLength(100);


    }
}