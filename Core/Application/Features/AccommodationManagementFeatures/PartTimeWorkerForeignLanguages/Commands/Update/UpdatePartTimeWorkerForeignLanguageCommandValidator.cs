using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Update;

public class UpdatePartTimeWorkerForeignLanguageCommandValidator : AbstractValidator<UpdatePartTimeWorkerForeignLanguageCommand>
{
    public UpdatePartTimeWorkerForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();
RuleFor(c => c.GidForeignLanguageFK).NotNull().NotEmpty();



    }
}