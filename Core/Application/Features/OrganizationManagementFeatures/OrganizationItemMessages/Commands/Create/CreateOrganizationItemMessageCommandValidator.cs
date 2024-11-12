using FluentValidation;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Commands.Create;

public class CreateOrganizationItemMessageCommandValidator : AbstractValidator<CreateOrganizationItemMessageCommand>
{
    public CreateOrganizationItemMessageCommandValidator()
    {
        RuleFor(c => c.GidOrganizationItemFK).NotNull().NotEmpty();
RuleFor(c => c.GidSendMessageUserFK).NotNull().NotEmpty();

RuleFor(c => c.Message).NotNull().NotEmpty().MaximumLength(150);


    }
}