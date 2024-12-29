using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;

public class GuestAccommodationPersonBusinessRules : BaseBusinessRules
{
    public async Task GuestAccommodationPersonShouldExistWhenSelected(X.GuestAccommodationPerson? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationPersonsBusinessMessages.GuestAccommodationPersonNotExists);
    }
}