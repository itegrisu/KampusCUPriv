using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;

public class GuestAccommodationPersonBusinessRules : BaseBusinessRules
{
    private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
    private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
    public GuestAccommodationPersonBusinessRules(IGuestAccommodationReadRepository guestAccommodationReadRepository, IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository)
    {
        _guestAccommodationReadRepository = guestAccommodationReadRepository;
        _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
    }

    public async Task GuestAccommodationPersonShouldExistWhenSelected(X.GuestAccommodationPerson? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationPersonsBusinessMessages.GuestAccommodationPersonNotExists);
    }

    public async Task GuestCountShouldNotExceedAccommodationLimit(Guid accommodationGid)
    {
        // Konaklama bilgilerini al
        var accommodation = await _guestAccommodationReadRepository.GetAsync(
            predicate: x => x.Gid == accommodationGid,
            include: x => x.Include(y => y.GuestAccommodationPersons)
        );

        // Konaklama bulunamadýysa hata fýrlat
        if (accommodation == null)
            throw new BusinessException("Konaklama bilgisi bulunamadi");

        // Mevcut konuk sayýsýný kontrol et
        var currentGuestCount = accommodation.GuestAccommodationPersons?.Count ?? 0;

        // Konuk sayýsýný belirlenen sýnýrla karþýlaþtýr
        if (currentGuestCount >= accommodation.GuestCount)
        {
            throw new BusinessException($"Maksimum konuk sayýsýna ({accommodation.GuestCount}) ulaþýldý. Daha fazla konuk eklenemez.");
        }
    }
}