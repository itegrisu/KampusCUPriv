using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Create;

public class CreateTaskFileCommandValidator : AbstractValidator<CreateTaskFileCommand>
{
    public CreateTaskFileCommandValidator()
    {
        RuleFor(c => c.GidTaskFK).NotEmpty();
        RuleFor(c => c.GidFileUploadUserFK).NotNull().NotEmpty();

        RuleFor(c => c.FileTitle).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.FileDescription).MaximumLength(250);
        RuleFor(c => c.UploadedFile).MaximumLength(150);


    }
}