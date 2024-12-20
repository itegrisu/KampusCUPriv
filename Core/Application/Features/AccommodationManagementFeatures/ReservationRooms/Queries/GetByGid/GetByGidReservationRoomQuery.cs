using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.ReservationRooms.Rules;
using Application.Repositories.AccommodationManagements.ReservationRoomRepo;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid
{
    public class GetByGidReservationRoomQuery : IRequest<GetByGidReservationRoomResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidReservationRoomQueryHandler : IRequestHandler<GetByGidReservationRoomQuery, GetByGidReservationRoomResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReservationRoomReadRepository _reservationRoomReadRepository;
            private readonly ReservationRoomBusinessRules _reservationRoomBusinessRules;

            public GetByGidReservationRoomQueryHandler(IMapper mapper, IReservationRoomReadRepository reservationRoomReadRepository, ReservationRoomBusinessRules reservationRoomBusinessRules)
            {
                _mapper = mapper;
                _reservationRoomReadRepository = reservationRoomReadRepository;
                _reservationRoomBusinessRules = reservationRoomBusinessRules;
            }

            public async Task<GetByGidReservationRoomResponse> Handle(GetByGidReservationRoomQuery request, CancellationToken cancellationToken)
            {
                X.ReservationRoom? reservationRoom = await _reservationRoomReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ReservationDetailFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _reservationRoomBusinessRules.ReservationRoomShouldExistWhenSelected(reservationRoom);

                GetByGidReservationRoomResponse response = _mapper.Map<GetByGidReservationRoomResponse>(reservationRoom);
                return response;
            }
        }
    }
}