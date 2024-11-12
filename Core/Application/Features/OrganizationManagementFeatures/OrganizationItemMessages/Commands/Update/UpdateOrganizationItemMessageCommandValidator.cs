using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Update;

public class UpdateOrganizationItemMessageCommandValidator : AbstractValidator<UpdateOrganizationItemMessageCommand>
{
    public UpdateOrganizationItemMessageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidOrganizationItemFK).NotNull().NotEmpty();
RuleFor(c => c.GidSendMessageUserFK).NotNull().NotEmpty();

RuleFor(c => c.Message).NotNull().NotEmpty().MaximumLength(150);


    }
}