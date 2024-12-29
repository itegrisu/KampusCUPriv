using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;

public class GuestAccommodationBusinessRules : BaseBusinessRules
{
    public async Task GuestAccommodationShouldExistWhenSelected(X.GuestAccommodation? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationsBusinessMessages.GuestAccommodationNotExists);
    }
}