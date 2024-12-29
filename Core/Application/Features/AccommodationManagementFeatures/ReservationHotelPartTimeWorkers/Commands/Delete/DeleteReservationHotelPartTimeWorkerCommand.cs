using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Delete;

public class DeleteReservationHotelPartTimeWorkerCommand : IRequest<DeletedReservationHotelPartTimeWorkerResponse>
{
    public Guid Gid { get; set; }

    public class DeleteReservationHotelPartTimeWorkerCommandHandler : IRequestHandler<DeleteReservationHotelPartTimeWorkerCommand, DeletedReservationHotelPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;
        private readonly IReservationHotelPartTimeWorkerWriteRepository _reservationHotelPartTimeWorkerWriteRepository;
        private readonly ReservationHotelPartTimeWorkerBusinessRules _reservationHotelPartTimeWorkerBusinessRules;

        public DeleteReservationHotelPartTimeWorkerCommandHandler(IMapper mapper, IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository,
                                         ReservationHotelPartTimeWorkerBusinessRules reservationHotelPartTimeWorkerBusinessRules, IReservationHotelPartTimeWorkerWriteRepository reservationHotelPartTimeWorkerWriteRepository)
        {
            _mapper = mapper;
            _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
            _reservationHotelPartTimeWorkerBusinessRules = reservationHotelPartTimeWorkerBusinessRules;
            _reservationHotelPartTimeWorkerWriteRepository = reservationHotelPartTimeWorkerWriteRepository;
        }

        public async Task<DeletedReservationHotelPartTimeWorkerResponse> Handle(DeleteReservationHotelPartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            X.ReservationHotelPartTimeWorker? reservationHotelPartTimeWorker = await _reservationHotelPartTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _reservationHotelPartTimeWorkerBusinessRules.ReservationHotelPartTimeWorkerShouldExistWhenSelected(reservationHotelPartTimeWorker);
            reservationHotelPartTimeWorker.DataState = Core.Enum.DataState.Deleted;

            _reservationHotelPartTimeWorkerWriteRepository.Update(reservationHotelPartTimeWorker);
            await _reservationHotelPartTimeWorkerWriteRepository.SaveAsync();

            return new()
            {
                Title = ReservationHotelPartTimeWorkersBusinessMessages.ProcessCompleted,
                Message = ReservationHotelPartTimeWorkersBusinessMessages.SuccessDeletedReservationHotelPartTimeWorkerMessage,
                IsValid = true
            };
        }
    }
}