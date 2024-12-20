using Application.Features.AccommodationManagementFeatures.ReservationRooms.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;

public class ReservationRoomBusinessRules : BaseBusinessRules
{
    public async Task ReservationRoomShouldExistWhenSelected(X.ReservationRoom? item)
    {
        if (item == null)
            throw new BusinessException(ReservationRoomsBusinessMessages.ReservationRoomNotExists);
    }
}