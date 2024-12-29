using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;

public class GuestAccommodationRoomBusinessRules : BaseBusinessRules
{
    public async Task GuestAccommodationRoomShouldExistWhenSelected(X.GuestAccommodationRoom? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationRoomsBusinessMessages.GuestAccommodationRoomNotExists);
    }
}