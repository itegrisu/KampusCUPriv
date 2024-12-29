using Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Rules;
using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid
{
    public class GetByGidReservationHotelPartTimeWorkerQuery : IRequest<GetByGidReservationHotelPartTimeWorkerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationHotelPartTimeWorkerQueryHandler : IRequestHandler<GetByGidReservationHotelPartTimeWorkerQuery, GetByGidReservationHotelPartTimeWorkerResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationHotelPartTimeWorkerReadRepository _reservationHotelPartTimeWorkerReadRepository;
            private readonly ReservationHotelPartTimeWorkerBusinessRules _reservationHotelPartTimeWorkerBusinessRules;

            public GetByGidReservationHotelPartTimeWorkerQueryHandler(IMapper mapper, IReservationHotelPartTimeWorkerReadRepository reservationHotelPartTimeWorkerReadRepository, ReservationHotelPartTimeWorkerBusinessRules reservationHotelPartTimeWorkerBusinessRules)
            {
                _mapper = mapper;
                _reservationHotelPartTimeWorkerReadRepository = reservationHotelPartTimeWorkerReadRepository;
                _reservationHotelPartTimeWorkerBusinessRules = reservationHotelPartTimeWorkerBusinessRules;
            }

            public async Task<GetByGidReservationHotelPartTimeWorkerResponse> Handle(GetByGidReservationHotelPartTimeWorkerQuery request, CancellationToken cancellationToken)
            {
                X.ReservationHotelPartTimeWorker? reservationHotelPartTimeWorker = await _reservationHotelPartTimeWorkerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationHotelPartTimeWorkerBusinessRules.ReservationHotelPartTimeWorkerShouldExistWhenSelected(reservationHotelPartTimeWorker);

                GetByGidReservationHotelPartTimeWorkerResponse response = _mapper.Map<GetByGidReservationHotelPartTimeWorkerResponse>(reservationHotelPartTimeWorker);
                return response;
            }
        }
    }
}