using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Application.Repositories.AccommodationManagements.GuestRepo;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;

public class AccommodationDateBusinessRules : BaseBusinessRules
{
    private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
    private readonly IGuestReadRepository _guestReadRepository;
    private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
    private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
    private readonly IReservationHotelReadRepository _reservationHotelReadRepository;


    public AccommodationDateBusinessRules(IReservationDetailReadRepository reservationDetailReadRepository, IGuestReadRepository guestReadRepository, IReservationRoomReadRepository reservationRoomReadRepository, IAccommodationDateReadRepository accommodationDateReadRepository, IReservationHotelReadRepository reservationHotelReadRepository)
    {
        _reservationDetailReadRepository = reservationDetailReadRepository;
        _guestReadRepository = guestReadRepository;
        _reservationRoomReadRepository = reservationRoomReadRepository;
        _accommodationDateReadRepository = accommodationDateReadRepository;
        _reservationHotelReadRepository = reservationHotelReadRepository;
    }

    public async Task AccommodationDateShouldExistWhenSelected(X.AccommodationDate? item)
    {
        if (item == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.AccommodationDateNotExists);
    }

    //GidReservationDetailFK
    public async Task IsReservationDetailFKExist(Guid gidReservationDetailFK)
    {
        if (await _reservationDetailReadRepository.GetByGidAsync(gidReservationDetailFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.ReservationDetailFKNotExists);
    }

    //GidReservationHotelFK
    public async Task IsReservationHotelFKExist(Guid gidReservationHotelFK)
    {
        if (await _reservationHotelReadRepository.GetByGidAsync(gidReservationHotelFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.ReservationHotelFKNotExists);
    }

    //GidGuestFK
    public async Task IsGuestFKExist(Guid gidGuestFK)
    {
        if (await _guestReadRepository.GetByGidAsync(gidGuestFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.GuestFKNotExists);
    }

    //GidRoomNoFK
    public async Task IsRoomNoFKExist(Guid gidRoomNoFK)
    {
        if (await _reservationRoomReadRepository.GetByGidAsync(gidRoomNoFK) == null)
            throw new BusinessException(AccommodationDatesBusinessMessages.RoomNoFKNotExists);
    }

    public async Task ValidateRoomTypeAvailability(Guid reservationDetailGid, DateTime[] dates, Guid roomNoGid)
    {
        var reservationDetail = await _reservationDetailReadRepository.GetByGidAsync(reservationDetailGid);
        var reservationRoom = await _reservationRoomReadRepository.GetAsync(
            predicate: x => x.Gid == roomNoGid,
            include: x => x.Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK)
        );

        var requestedRoomType = reservationRoom.ReservationDetailFK.RoomTypeFK.Name;

        var reservationDetails = await _reservationDetailReadRepository.GetAll()
            .Where(x => x.GidReservationHotelFK == reservationDetail.GidReservationHotelFK)
            .Include(x => x.RoomTypeFK)
            .ToListAsync();

        // Tarihleri oda tiplerine göre gruplandýr
        var reservedDates = reservationDetails
            .Where(rd => dates.Contains(rd.ReservationDate.Date))
            .GroupBy(rd => rd.ReservationDate.Date)
            .ToDictionary(
                group => group.Key,
                group => group.Select(rd => rd.RoomTypeFK.Name).ToList() // Ayný tarihteki oda tiplerini liste olarak al
            );

        foreach (var date in dates)
        {
            if (reservedDates.TryGetValue(date.Date, out var roomTypes) && !roomTypes.Contains(requestedRoomType))
            {
                throw new BusinessException(date.ToString("dd.MM.yyyy") + " Tarihinde " + requestedRoomType + " Oda Tipi Mevcut Deðil!");
            }
        }
    }


    public async Task GuestControl(Guid reservationDetailGid, Guid guestGid, DateTime[] dates)
    {
        var accommodationDates = await _accommodationDateReadRepository.GetAll()
            .Where(x => x.GidGuestFK == guestGid && dates.Contains(x.Date))
            .ToListAsync();

        if (accommodationDates.Any())
            throw new BusinessException(AccommodationDatesBusinessMessages.CustomerAlreadyRegisteredAnotherRoom);
    }

    public async Task ValidateRoomCapacity(Guid reservationDetailGid, Guid reservationRoomGid, DateTime[] dates)
    {
        var reservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: x => x.Gid == reservationRoomGid,
          include: x => x.Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK));

        foreach (var date in dates)
        {
            var accommodationDates = await _accommodationDateReadRepository.GetAll().Where(x => x.GidReservationDetailFK == reservationDetailGid && x.Date == date
            && x.GidRoomNoFK == reservationRoomGid).Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK).ToListAsync();

            if (accommodationDates.Count() >= reservationRoom.ReservationDetailFK.RoomTypeFK.Capacity)
                throw new BusinessException(date.ToString("dd.MM.yyyy") + " Tarihinde Oda Kapasitesi Aþýlmaktadýr!");
        }
    }
}