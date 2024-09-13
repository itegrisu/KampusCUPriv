using FluentValidation;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Update;

public class UpdateLogAuthorizationErrorCommandValidator : AbstractValidator<UpdateLogAuthorizationErrorCommand>
{
    public UpdateLogAuthorizationErrorCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidUserFK);//

RuleFor(c => c.IpAddress).MaximumLength(20);
RuleFor(c => c.PageInfo).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.Operation).MaximumLength(100);
RuleFor(c => c.JSonData).MaximumLength(2000);


    }
}