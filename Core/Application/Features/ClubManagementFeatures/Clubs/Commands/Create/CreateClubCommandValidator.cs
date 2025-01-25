using FluentValidation;

namespace Application.Features.ClubFeatures.Clubs.Commands.Create;

public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        //RuleFor(c => c.GidManagerFK);//
        RuleFor(c => c.GidCategoryFK).NotNull().NotEmpty();
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Logo).MaximumLength(500);
        RuleFor(c => c.Description).MaximumLength(250);
        RuleFor(c => c.Color).MaximumLength(8);
    }
}