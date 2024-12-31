using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Constants;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Core.Enum;
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
            include: x => x.Include(y => y.GuestAccommodationPersons.Where(p => p.DataState == DataState.Active)) // Sadece aktif olanlarý dahil et
        );

        // Konaklama bulunamadýysa hata fýrlat
        if (accommodation == null)
            throw new BusinessException("Konaklama bilgisi bulunamadi");

        // Mevcut aktif konuk sayýsýný kontrol et
        var currentGuestCount = accommodation.GuestAccommodationPersons?.Count ?? 0;

        // Konuk sayýsýný belirlenen sýnýrla karþýlaþtýr
        if (currentGuestCount >= accommodation.GuestCount)
        {
            throw new BusinessException($"Maksimum konuk sayýsýna ({accommodation.GuestCount}) ulaþýldý. Daha fazla konuk eklenemez.");
        }
    }

    public async Task GuestAccommodationPersonShouldNotBeDeletedIfAssignedToRoom(Guid personGid)
    {
        // Misafir bilgilerini al
        var guestPerson = await _guestAccommodationPersonReadRepository.GetAsync(
            predicate: x => x.Gid == personGid,
            include: x => x.Include(y => y.GuestAccommodationResults.Where(p => p.DataState == DataState.Active) )
        );

        // Misafir bulunamadýysa hata fýrlat
        if (guestPerson == null)
            throw new BusinessException("Misafir bilgisi bulunamadý");

        // Misafir bir odaya atanmýþsa hata fýrlat
        if (guestPerson.GuestAccommodationResults != null && guestPerson.GuestAccommodationResults.Any())
        {
            throw new BusinessException("Bu misafir bir odada kayýtlý olduðu için silinemez.");
        }
    }
}