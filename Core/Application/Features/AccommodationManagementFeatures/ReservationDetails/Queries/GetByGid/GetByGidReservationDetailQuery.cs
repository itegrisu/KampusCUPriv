using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.ReservationDetails.Rules;
using Application.Repositories.AccommodationManagements.ReservationDetailRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid
{
    public class GetByGidReservationDetailQuery : IRequest<GetByGidReservationDetailResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationDetailQueryHandler : IRequestHandler<GetByGidReservationDetailQuery, GetByGidReservationDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDetailReadRepository _reservationDetailReadRepository;
            private readonly ReservationDetailBusinessRules _reservationDetailBusinessRules;

            public GetByGidReservationDetailQueryHandler(IMapper mapper, IReservationDetailReadRepository reservationDetailReadRepository, ReservationDetailBusinessRules reservationDetailBusinessRules)
            {
                _mapper = mapper;
                _reservationDetailReadRepository = reservationDetailReadRepository;
                _reservationDetailBusinessRules = reservationDetailBusinessRules;
            }

            public async Task<GetByGidReservationDetailResponse> Handle(GetByGidReservationDetailQuery request, CancellationToken cancellationToken)
            {
                X.ReservationDetail? reservationDetail = await _reservationDetailReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ReservationHotelFK).Include(X => X.RoomTypeFK).Include(x => x.ReservationHotelFK).ThenInclude(x => x.ReservationFK));
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationDetailBusinessRules.ReservationDetailShouldExistWhenSelected(reservationDetail);

                GetByGidReservationDetailResponse response = _mapper.Map<GetByGidReservationDetailResponse>(reservationDetail);
                return response;
            }
        }
    }
}