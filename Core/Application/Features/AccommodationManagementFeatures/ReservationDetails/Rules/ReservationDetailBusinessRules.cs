using Application.Features.AccommodationManagementFeatures.ReservationDetails.Constants;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using Core.Enum;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;

public class ReservationDetailBusinessRules : BaseBusinessRules
{
    private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
    private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
    private readonly IRoomTypeReadRepository _roomTypeReadRepository;

    public ReservationDetailBusinessRules(IReservationHotelReadRepository reservationHotelReadRepository, IReservationDetailReadRepository reservationDetailReadRepository, IRoomTypeReadRepository roomTypeReadRepository)
    {
        _reservationHotelReadRepository = reservationHotelReadRepository;
        _reservationDetailReadRepository = reservationDetailReadRepository;
        _roomTypeReadRepository = roomTypeReadRepository;
    }

    public async Task ReservationDetailShouldExistWhenSelected(X.ReservationDetail? item)
    {
        if (item == null)
            throw new BusinessException(ReservationDetailsBusinessMessages.ReservationDetailNotExists);
    }

    public async Task RoomCountCanBeGreaterZero(int roomCount)
    {
        if (roomCount <= 0)
            throw new BusinessException(ReservationDetailsBusinessMessages.RoomCountCanBeGreaterThanZero);
    }

    public async Task ReservationDateControl(Guid reservationHotelGid, DateTime reservationDate)
    {
        var reservationHotel = await _reservationHotelReadRepository.GetWhere(x => x.Gid == reservationHotelGid).Include(x => x.ReservationFK).FirstOrDefaultAsync();

        if (reservationDate < reservationHotel.ReservationFK.StartDate || reservationDate > reservationHotel.ReservationFK.EndDate)
            throw new BusinessException(ReservationDetailsBusinessMessages.DateMustBeBetweenResDate);
    }

    public async Task ReservationDateCanBeUniq(Guid reservationHotelGid, Guid roomTypeGid, DateTime reservationDate)
    {
        var reservationDateOnly = reservationDate.Date; // Saat bilgisini sýfýrla, yalnýzca tarihi al

        var reservation = await _reservationDetailReadRepository
            .GetWhere(x => x.GidReservationHotelFK == reservationHotelGid &&
                           x.GidRoomTypeFK == roomTypeGid &&
                           x.ReservationDate.Date == reservationDateOnly) // Tarih bazlý karþýlaþtýrma
            .FirstOrDefaultAsync();

        if (reservation != null)
            throw new BusinessException(ReservationDetailsBusinessMessages.ReservationDateCanBeUniq);
    }


    public async Task IsThereSelectedHotel(Guid gidReservationHotelFK)
    {
        if (await _reservationHotelReadRepository.GetByGidAsync(gidReservationHotelFK) == null)
            throw new BusinessException(ReservationDetailsBusinessMessages.ReservationHotelNotExist);
    }

    public async Task IsThereSelectedRoomType(Guid gidRoomTypeFK)
    {
        if (await _roomTypeReadRepository.GetByGidAsync(gidRoomTypeFK) == null)
            throw new BusinessException(ReservationDetailsBusinessMessages.RoomTypeNotExist);
    }

    public async Task PartTimeWorkerControl(string gidReservationHotelFK, Guid partTimeWorkerGid)
    {
        var reservationHotels = await _reservationHotelReadRepository.GetAll()
            .Where(x => x.ReservationHotelPartTimeWorkers.Any(r => r.GidPartTimeWorkerFK == partTimeWorkerGid
                && r.DataState == DataState.Active && r.IsActive == true))
            .Include(x => x.ReservationHotelPartTimeWorkers).Include(x => x.ReservationFK).ToListAsync();

        if (!reservationHotels.Any(x => x.Gid.ToString() == gidReservationHotelFK))
            throw new BusinessException(ReservationDetailsBusinessMessages.ReservationHotelAuthError);

    }

}