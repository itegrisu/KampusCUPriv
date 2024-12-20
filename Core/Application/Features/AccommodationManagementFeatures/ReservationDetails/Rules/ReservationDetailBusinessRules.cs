using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;

public class ReservationDetailBusinessRules : BaseBusinessRules
{
    public async Task ReservationDetailShouldExistWhenSelected(X.ReservationDetail? item)
    {
        if (item == null)
            throw new BusinessException(ReservationDetailsBusinessMessages.ReservationDetailNotExists);
    }
}