using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;

public class ReservationHotelPartTimeWorkerBusinessRules : BaseBusinessRules
{
    public async Task ReservationHotelPartTimeWorkerShouldExistWhenSelected(X.ReservationHotelPartTimeWorker? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelPartTimeWorkersBusinessMessages.ReservationHotelPartTimeWorkerNotExists);
    }
}