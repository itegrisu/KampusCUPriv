using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Update;

public class UpdateSupportMessageDetailCommandValidator : AbstractValidator<UpdateSupportMessageDetailCommand>
{
    public UpdateSupportMessageDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidSupportFK).NotNull().NotEmpty();
        RuleFor(c => c.GidReadUserFK).NotNull().NotEmpty();

        RuleFor(c => c.ReadDate).NotNull().NotEmpty();
        RuleFor(c => c.ReadIp).NotNull().NotEmpty().MaximumLength(20);


    }
}