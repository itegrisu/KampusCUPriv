using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.PartTimeWorkers.Commands.Create;

public class CreatePartTimeWorkerCommandValidator : AbstractValidator<CreatePartTimeWorkerCommand>
{
    public CreatePartTimeWorkerCommandValidator()
    {
        
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