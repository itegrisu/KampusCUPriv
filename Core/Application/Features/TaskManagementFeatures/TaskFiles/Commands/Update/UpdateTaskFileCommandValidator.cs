using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Update;

public class UpdateTaskFileCommandValidator : AbstractValidator<UpdateTaskFileCommand>
{
    public UpdateTaskFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.FileTitle).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidTaskFK).NotEmpty();
         RuleFor(c => c.GidFileUploadUserFK).NotEmpty();
        RuleFor(c => c.FileDescription).MaximumLength(250);
        RuleFor(c => c.UploadedFile).MaximumLength(150);


    }
}