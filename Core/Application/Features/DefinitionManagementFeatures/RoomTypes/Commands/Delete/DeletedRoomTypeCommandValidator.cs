using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Delete;

public class DeleteRoomTypeCommandValidator : AbstractValidator<DeleteRoomTypeCommand>
{
    public DeleteRoomTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}