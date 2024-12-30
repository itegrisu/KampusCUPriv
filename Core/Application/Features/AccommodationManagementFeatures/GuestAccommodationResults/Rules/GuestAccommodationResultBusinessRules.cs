using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Constants;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;

public class GuestAccommodationResultBusinessRules : BaseBusinessRules
{
    private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
    private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;

    public GuestAccommodationResultBusinessRules(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository)
    {
        _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
        _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
    }

    public async Task GuestAccommodationResultShouldExistWhenSelected(X.GuestAccommodationResult? item)
    {
        if (item == null)
            throw new BusinessException(GuestAccommodationResultsBusinessMessages.GuestAccommodationResultNotExists);
    }

    public async Task RoomCapacityShouldNotBeExceeded(Guid roomGid)
    {
        var room = await _guestAccommodationRoomReadRepository.GetAsync(
            predicate: x => x.Gid == roomGid,
            include: x => x.Include(y => y.RoomTypeFK)
        );

        if (room == null || room.RoomTypeFK == null)
        {
            throw new BusinessException("Oda bilgisi veya oda tipi bilgisi bulunamadı.");
        }

        var roomCapacity = room.RoomTypeFK.Capacity;

        // İlgili odadaki mevcut doluluğu kontrol et
        var roomResults = await _guestAccommodationResultReadRepository.GetListAsync(
            predicate: x => x.GidGuestAccommodationRoomFK == roomGid,
            include: x => x.Include(y => y.GuestAccommodationRoomFK).ThenInclude(z => z.RoomTypeFK)
        );

        var currentOccupancy = roomResults.Items.Count;

        if (currentOccupancy >= roomCapacity)
        {
            throw new BusinessException($"Oda kapasitesi ({roomCapacity}) dolmuş durumda. Yeni kişi eklenemez.");
        }
    }


    public async Task GuestCannotBeAddedTwiceToSameRoom(Guid guestId, Guid roomId)
    {
        var existingResult = await _guestAccommodationResultReadRepository.GetAsync(
            predicate: x => x.GidGuestAccommodationPersonFK == guestId && x.GidGuestAccommodationRoomFK == roomId
        );

        if (existingResult != null)
        {
            throw new BusinessException("Aynı misafir bu odaya zaten eklenmiş. Tekrar eklenemez.");
        }
    }

    public async Task GuestCannotBeAddedToMultipleRoomsOnSameDay(Guid guestId, DateTime date)
    {
        var existingResult = await _guestAccommodationResultReadRepository.GetAsync(
            predicate: x => x.GidGuestAccommodationPersonFK == guestId && x.GuestAccommodationRoomFK.Date.Date == date.Date
        );

        if (existingResult != null)
        {
            throw new BusinessException("Aynı misafir aynı gün içerisinde sadece bir odaya eklenebilir.");
        }
    }
}