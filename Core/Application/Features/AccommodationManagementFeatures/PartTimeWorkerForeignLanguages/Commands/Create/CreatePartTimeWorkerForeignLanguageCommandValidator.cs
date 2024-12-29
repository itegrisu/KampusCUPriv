using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerForeignLanguages.Commands.Create;

public class CreatePartTimeWorkerForeignLanguageCommandValidator : AbstractValidator<CreatePartTimeWorkerForeignLanguageCommand>
{
    public CreatePartTimeWorkerForeignLanguageCommandValidator()
    {
        RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();
RuleFor(c => c.GidForeignLanguageFK).NotNull().NotEmpty();



    }
}