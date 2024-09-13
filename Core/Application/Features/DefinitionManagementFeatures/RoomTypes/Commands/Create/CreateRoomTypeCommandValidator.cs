using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Commands.Create;

public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator()
    {
        
RuleFor(c => c.OdaTuru).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.OdaKodu).NotNull().NotEmpty().MaximumLength(10);
RuleFor(c => c.KisiSayisi).NotNull().NotEmpty();
RuleFor(c => c.Aciklama).MaximumLength(100);


    }
}