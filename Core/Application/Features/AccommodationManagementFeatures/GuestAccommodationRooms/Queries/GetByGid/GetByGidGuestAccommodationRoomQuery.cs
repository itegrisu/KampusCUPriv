using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Rules;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid
{
    public class GetByGidGuestAccommodationRoomQuery : IRequest<GetByGidGuestAccommodationRoomResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidGuestAccommodationRoomQueryHandler : IRequestHandler<GetByGidGuestAccommodationRoomQuery, GetByGidGuestAccommodationRoomResponse>
        {
            private readonly IMapper _mapper;
            private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
            private readonly GuestAccommodationRoomBusinessRules _guestAccommodationRoomBusinessRules;

            public GetByGidGuestAccommodationRoomQueryHandler(IMapper mapper, IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository, GuestAccommodationRoomBusinessRules guestAccommodationRoomBusinessRules)
            {
                _mapper = mapper;
                _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
                _guestAccommodationRoomBusinessRules = guestAccommodationRoomBusinessRules;
            }

            public async Task<GetByGidGuestAccommodationRoomResponse> Handle(GetByGidGuestAccommodationRoomQuery request, CancellationToken cancellationToken)
            {
                X.GuestAccommodationRoom? guestAccommodationRoom = await _guestAccommodationRoomReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _guestAccommodationRoomBusinessRules.GuestAccommodationRoomShouldExistWhenSelected(guestAccommodationRoom);

                GetByGidGuestAccommodationRoomResponse response = _mapper.Map<GetByGidGuestAccommodationRoomResponse>(guestAccommodationRoom);
                return response;
            }
        }
    }
}