using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Constants;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;

public class GuestAccommodationBusinessRules : BaseBusinessRules
{
    private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;

    public GuestAccommodationBusinessRules(IGuestAccommodationReadRepository guestAccommodationReadRepository)
    {
        _guestAccommodationReadRepository = guestAccommodationReadRepository;
    }

    public async Task GuestAccommodationShouldExistWhenSelected(X.GuestAccommodation? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationsBusinessMessages.GuestAccommodationNotExists);
    }

 
}