using Application.Features.AccommodationManagementFeatures.ReservationHotels.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Rules;

public class ReservationHotelBusinessRules : BaseBusinessRules
{
    public async Task ReservationHotelShouldExistWhenSelected(X.ReservationHotel? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelsBusinessMessages.ReservationHotelNotExists);
    }
}