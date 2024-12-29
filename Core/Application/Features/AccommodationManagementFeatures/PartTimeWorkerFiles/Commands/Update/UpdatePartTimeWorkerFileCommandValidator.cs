using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkerFiles.Commands.Update;

public class UpdatePartTimeWorkerFileCommandValidator : AbstractValidator<UpdatePartTimeWorkerFileCommand>
{
    public UpdatePartTimeWorkerFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.WorkerFile).MaximumLength(150);


    }
}