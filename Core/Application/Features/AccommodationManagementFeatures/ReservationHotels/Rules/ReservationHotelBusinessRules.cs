using Application.Features.AccommodationManagementFeatures.ReservationHotels.Constants;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;

public class ReservationHotelBusinessRules : BaseBusinessRules
{
    private readonly IReservationHotelReadRepository _reservationHotelReadRepository;

    public ReservationHotelBusinessRules(IReservationHotelReadRepository reservationHotelReadRepository)
    {
        _reservationHotelReadRepository = reservationHotelReadRepository;
    }

    public async Task ReservationHotelShouldExistWhenSelected(X.ReservationHotel? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelsBusinessMessages.ReservationHotelNotExists);
    }

    public async Task ReservationHotelAlreadyAdded(Guid reservationGid, Guid hotelGid)
    {
        var reservationHotel = await _reservationHotelReadRepository.GetAll().Where(x => x.GidReservationFK == reservationGid && x.GidHotelFK == hotelGid).FirstOrDefaultAsync();
        if (reservationHotel != null)
            throw new BusinessException(ReservationHotelsBusinessMessages.ReservationHotelAlreadyAdded);
    }

}