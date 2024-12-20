using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;

public class AccommodationDateBusinessRules : BaseBusinessRules
{
    public async Task AccommodationDateShouldExistWhenSelected(X.AccommodationDate? item)
    {
        if (item == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.AccommodationDateNotExists);
    }
}