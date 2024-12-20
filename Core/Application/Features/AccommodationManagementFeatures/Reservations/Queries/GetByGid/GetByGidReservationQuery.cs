using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.Reservations.Rules;
using Application.Repositories.AccommodationManagements.ReservationRepo;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Queries.GetByGid
{
    public class GetByGidReservationQuery : IRequest<GetByGidReservationResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationQueryHandler : IRequestHandler<GetByGidReservationQuery, GetByGidReservationResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationReadRepository _reservationReadRepository;
            private readonly ReservationBusinessRules _reservationBusinessRules;

            public GetByGidReservationQueryHandler(IMapper mapper, IReservationReadRepository reservationReadRepository, ReservationBusinessRules reservationBusinessRules)
            {
                _mapper = mapper;
                _reservationReadRepository = reservationReadRepository;
                _reservationBusinessRules = reservationBusinessRules;
            }

            public async Task<GetByGidReservationResponse> Handle(GetByGidReservationQuery request, CancellationToken cancellationToken)
            {
                X.Reservation? reservation = await _reservationReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OrganizationFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);

                GetByGidReservationResponse response = _mapper.Map<GetByGidReservationResponse>(reservation);
                return response;
            }
        }
    }
}