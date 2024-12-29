using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Update;

public class UpdatePartTimeWorkerCommandValidator : AbstractValidator<UpdatePartTimeWorkerCommand>
{
    public UpdatePartTimeWorkerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.IdentityNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(60);
RuleFor(c => c.UserName).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(255);
RuleFor(c => c.PasswordHash).NotNull().NotEmpty().MaximumLength(255);
RuleFor(c => c.IsLoginStatus).NotNull().NotEmpty();
RuleFor(c => c.Gsm).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.BirthDate).NotNull().NotEmpty();


    }
}