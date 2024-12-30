using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Update;

public class UpdateReservationHotelPartTimeWorkerCommand : IRequest<UpdatedReservationHotelPartTimeWorkerResponse>
{
    public Guid Gid { get; set; }

    public Guid GidHotelFK { get; set; }
    public Guid GidPartTimeWorkerFK { get; set; }

    public bool IsActive { get; set; }
    public string? Note { get; set; }



    public class UpdateReservationHotelPartTimeWorkerCommandHandler : IRequestHandler<UpdateReservationHotelPartTimeWorkerCommand, UpdatedReservationHotelPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelPartTimeWorkerWriteRepository _reservationHotelPartTimeWorkerWriteRepository;
        private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;
        private readonly ReservationHotelPartTimeWorkerBusinessRules _reservationHotelPartTimeWorkerBusinessRules;

        public UpdateReservationHotelPartTimeWorkerCommandHandler(IMapper mapper, IReservationHotelPartTimeWorkerWriteRepository reservationHotelPartTimeWorkerWriteRepository,
                                         ReservationHotelPartTimeWorkerBusinessRules reservationHotelPartTimeWorkerBusinessRules, IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository)
        {
            _mapper = mapper;
            _reservationHotelPartTimeWorkerWriteRepository = reservationHotelPartTimeWorkerWriteRepository;
            _reservationHotelPartTimeWorkerBusinessRules = reservationHotelPartTimeWorkerBusinessRules;
            _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
        }

        public async Task<UpdatedReservationHotelPartTimeWorkerResponse> Handle(UpdateReservationHotelPartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            await _reservationHotelPartTimeWorkerBusinessRules.PartTimeWorkerAlreadyExist(request.GidPartTimeWorkerFK);
            await _reservationHotelPartTimeWorkerBusinessRules.ReservationHotelAlreadyExist(request.GidHotelFK);

            X.ReservationHotelPartTimeWorker? reservationHotelPartTimeWorker = await _reservationHotelPartTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);


            await _reservationHotelPartTimeWorkerBusinessRules.ReservationHotelPartTimeWorkerShouldExistWhenSelected(reservationHotelPartTimeWorker);
            reservationHotelPartTimeWorker = _mapper.Map(request, reservationHotelPartTimeWorker);

            _reservationHotelPartTimeWorkerWriteRepository.Update(reservationHotelPartTimeWorker!);
            await _reservationHotelPartTimeWorkerWriteRepository.SaveAsync();
            GetByGidReservationHotelPartTimeWorkerResponse obj = _mapper.Map<GetByGidReservationHotelPartTimeWorkerResponse>(reservationHotelPartTimeWorker);

            return new()
            {
                Title = ReservationHotelPartTimeWorkersBusinessMessages.ProcessCompleted,
                Message = ReservationHotelPartTimeWorkersBusinessMessages.SuccessCreatedReservationHotelPartTimeWorkerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}