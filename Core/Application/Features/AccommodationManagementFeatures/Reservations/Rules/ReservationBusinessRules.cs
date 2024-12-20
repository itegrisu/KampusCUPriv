using Application.Features.AccommodationManagementFeatures.Reservations.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Rules;

public class ReservationBusinessRules : BaseBusinessRules
{
    public async Task ReservationShouldExistWhenSelected(X.Reservation? item)
    {
        if (item == null)
            throw new BusinessException(ReservationsBusinessMessages.ReservationNotExists);
    }
}