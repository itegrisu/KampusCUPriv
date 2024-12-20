using Application.Features.AccommodationManagementFeatures.Guests.Constants;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Guests.Rules;

public class GuestBusinessRules : BaseBusinessRules
{
    public async Task GuestShouldExistWhenSelected(X.Guest? item)
    {
        if (item == null)
            throw new BusinessException(GuestsBusinessMessages.GuestNotExists);
    }
}