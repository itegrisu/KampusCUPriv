using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Constants;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;

public class GuestAccommodationRoomBusinessRules : BaseBusinessRules
{
    private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;

    public GuestAccommodationRoomBusinessRules(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository)
    {
        _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
    }
    public async Task GuestAccommodationRoomShouldExistWhenSelected(X.GuestAccommodationRoom? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationRoomsBusinessMessages.GuestAccommodationRoomNotExists);
    }

    public async Task GuestAccommodationRoomCannotBeDeletedIfOccupied(Guid roomGid)
    {
        var occupancyExists = await _guestAccommodationResultReadRepository.GetListAsync(
            predicate: x => x.GidGuestAccommodationRoomFK == roomGid
        );

        if (occupancyExists.Items.Count != 0)
        {
            throw new BusinessException("Bu odada konaklayan kiþiler mevcutken oda silinemez.");
        }
    }
}