using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Create;

public class CreatePartTimeWorkerFileCommandValidator : AbstractValidator<CreatePartTimeWorkerFileCommand>
{
    public CreatePartTimeWorkerFileCommandValidator()
    {
        RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.WorkerFile).MaximumLength(150);


    }
}