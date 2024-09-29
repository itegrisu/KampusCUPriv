using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;

public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator()
    {

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Code).NotNull().NotEmpty().MaximumLength(10);
        RuleFor(c => c.Capacity).NotNull().NotEmpty();
        RuleFor(c => c.Description).MaximumLength(100);


    }
}