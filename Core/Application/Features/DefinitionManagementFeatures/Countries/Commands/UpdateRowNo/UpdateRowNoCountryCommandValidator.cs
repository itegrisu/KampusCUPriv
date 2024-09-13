using Application.Features.DefinitionManagementFeatures.Countries.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;

public class UpdateRowNoCountryCommandValidator : AbstractValidator<UpdateRowNoCountryCommand>
{
    public UpdateRowNoCountryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}