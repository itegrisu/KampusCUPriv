using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Create;

public class CreateLogUserPageVisitActionCommandValidator : AbstractValidator<CreateLogUserPageVisitActionCommand>
{
    public CreateLogUserPageVisitActionCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.PageInfo).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.Operation).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.JSonData).NotNull().NotEmpty().MaximumLength(2000);


    }
}