using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Repositories.AccommodationManagements.GuestRepo;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;

public class AccommodationDateBusinessRules : BaseBusinessRules
{
    private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
    private readonly IGuestReadRepository _guestReadRepository;
    private readonly IReservationRoomReadRepository _reservationRoomReadRepository;

    public AccommodationDateBusinessRules(IReservationDetailReadRepository reservationDetailReadRepository, IGuestReadRepository guestReadRepository, IReservationRoomReadRepository reservationRoomReadRepository)
    {
        _reservationDetailReadRepository = reservationDetailReadRepository;
        _guestReadRepository = guestReadRepository;
        _reservationRoomReadRepository = reservationRoomReadRepository;
    }

    public async Task AccommodationDateShouldExistWhenSelected(X.AccommodationDate? item)
    {
        if (item == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.AccommodationDateNotExists);
    }

    //GidReservationDetailFK
    public async Task IsReservationDetailFKExist(Guid gidReservationDetailFK)
    {
        if (await _reservationDetailReadRepository.GetByGidAsync(gidReservationDetailFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.ReservationDetailFKNotExists);
    }

    //GidGuestFK
    public async Task IsGuestFKExist(Guid gidGuestFK)
    {
        if (await _guestReadRepository.GetByGidAsync(gidGuestFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.GuestFKNotExists);
    }

    //GidRoomNoFK
    public async Task IsRoomNoFKExist(Guid gidRoomNoFK)
    {
        if (await _reservationRoomReadRepository.GetByGidAsync(gidRoomNoFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.RoomNoFKNotExists);
    }

}