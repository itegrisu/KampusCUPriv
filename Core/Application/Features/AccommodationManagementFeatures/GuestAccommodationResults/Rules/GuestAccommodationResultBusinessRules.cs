using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;

public class GuestAccommodationResultBusinessRules : BaseBusinessRules
{
    public async Task GuestAccommodationResultShouldExistWhenSelected(X.GuestAccommodationResult? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationResultsBusinessMessages.GuestAccommodationResultNotExists);
    }
}