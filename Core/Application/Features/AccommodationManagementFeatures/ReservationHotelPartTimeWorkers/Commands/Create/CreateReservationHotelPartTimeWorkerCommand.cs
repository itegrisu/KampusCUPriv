using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Constants;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Create;

public class CreateReservationHotelPartTimeWorkerCommand : IRequest<CreatedReservationHotelPartTimeWorkerResponse>
{
    public Guid GidHotelFK { get; set; }
    public Guid GidPartTimeWorkerFK { get; set; }

    public bool IsActive { get; set; }
    public string? Note { get; set; }



    public class CreateReservationHotelPartTimeWorkerCommandHandler : IRequestHandler<CreateReservationHotelPartTimeWorkerCommand, CreatedReservationHotelPartTimeWorkerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationHotelPartTimeWorkerWriteRepository _reservationHotelPartTimeWorkerWriteRepository;
        private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;
        private readonly ReservationHotelPartTimeWorkerBusinessRules _reservationHotelPartTimeWorkerBusinessRules;

        public CreateReservationHotelPartTimeWorkerCommandHandler(IMapper mapper, IReservationHotelPartTimeWorkerWriteRepository reservationHotelPartTimeWorkerWriteRepository,
                                         ReservationHotelPartTimeWorkerBusinessRules reservationHotelPartTimeWorkerBusinessRules, IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository)
        {
            _mapper = mapper;
            _reservationHotelPartTimeWorkerWriteRepository = reservationHotelPartTimeWorkerWriteRepository;
            _reservationHotelPartTimeWorkerBusinessRules = reservationHotelPartTimeWorkerBusinessRules;
            _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
        }

        public async Task<CreatedReservationHotelPartTimeWorkerResponse> Handle(CreateReservationHotelPartTimeWorkerCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _reservationHotelPartTimeWorkerReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.ReservationHotelPartTimeWorker reservationHotelPartTimeWorker = _mapper.Map<X.ReservationHotelPartTimeWorker>(request);
            //reservationHotelPartTimeWorker.RowNo = maxRowNo + 1;

            await _reservationHotelPartTimeWorkerWriteRepository.AddAsync(reservationHotelPartTimeWorker);
            await _reservationHotelPartTimeWorkerWriteRepository.SaveAsync();

            X.ReservationHotelPartTimeWorker savedReservationHotelPartTimeWorker = await _reservationHotelPartTimeWorkerReadRepository.GetAsync(predicate: x => x.Gid == reservationHotelPartTimeWorker.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidReservationHotelPartTimeWorkerResponse obj = _mapper.Map<GetByGidReservationHotelPartTimeWorkerResponse>(savedReservationHotelPartTimeWorker);
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