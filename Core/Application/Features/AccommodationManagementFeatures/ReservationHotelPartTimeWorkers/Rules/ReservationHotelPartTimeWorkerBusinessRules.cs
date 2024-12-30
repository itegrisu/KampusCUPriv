using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Constants;
using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;

public class ReservationHotelPartTimeWorkerBusinessRules : BaseBusinessRules
{
    private readonly IPartTimeWorkerReadRepository _partTimeWorkerReadRepository;
    private readonly IReservationHotelReadRepository _reservationHotelReadRepository;
    private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;

    public ReservationHotelPartTimeWorkerBusinessRules(IPartTimeWorkerReadRepository partTimeWorkerReadRepository, IReservationHotelReadRepository reservationHotelReadRepository, IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository)
    {
        _partTimeWorkerReadRepository = partTimeWorkerReadRepository;
        _reservationHotelReadRepository = reservationHotelReadRepository;
        _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
    }

    public async Task ReservationHotelPartTimeWorkerShouldExistWhenSelected(X.ReservationHotelPartTimeWorker? item)
    {
        if (item == null)
            throw new BusinessException(ReservationHotelPartTimeWorkersBusinessMessages.ReservationHotelPartTimeWorkerNotExists);
    }

    public async Task PartTimeWorkerAlreadyExist(Guid partTimeWorkerGid)
    {
        if (await _partTimeWorkerReadRepository.GetByGidAsync(partTimeWorkerGid) == null)
            throw new BusinessException(ReservationHotelPartTimeWorkersBusinessMessages.PartTimeWorkerNotExists);
    }

    public async Task ReservationHotelAlreadyExist(Guid reservationHotelGid)
    {
        if (await _reservationHotelReadRepository.GetByGidAsync(reservationHotelGid) == null)
            throw new BusinessException(ReservationHotelPartTimeWorkersBusinessMessages.ReservationHotelNotExists);
    }

    public async Task IsReservationHotelAlreadyAdded(Guid partTimeWorkerGid, Guid reservationHotelGid)
    {
        if (await _reservationHotelPartTimeWorkerReadRepository.GetAsync(x => x.GidPartTimeWorkerFK == partTimeWorkerGid && x.GidHotelFK == reservationHotelGid) != null)
            throw new BusinessException(ReservationHotelPartTimeWorkersBusinessMessages.PartTimeWorkerAlreadyExist);
    }

}