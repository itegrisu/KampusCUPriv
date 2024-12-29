using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;

public class GetListGuestAccommodationRoomQuery : IRequest<GetListResponse<GetListGuestAccommodationRoomListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGuestAccommodationRoomQueryHandler : IRequestHandler<GetListGuestAccommodationRoomQuery, GetListResponse<GetListGuestAccommodationRoomListItemDto>>
    {
        private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.GuestAccommodationRoom, GetListGuestAccommodationRoomListItemDto> _noPagination;

        public GetListGuestAccommodationRoomQueryHandler(IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationRoom, GetListGuestAccommodationRoomListItemDto> noPagination)
        {
            _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListGuestAccommodationRoomListItemDto>> Handle(GetListGuestAccommodationRoomQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<GuestAccommodationRoom, object>>[]
                    {
                       x => x.GuestAccommodationFK,
                       x=> x.RoomTypeFK
                    });
            IPaginate<X.GuestAccommodationRoom> guestAccommodationRooms = await _guestAccommodationRoomReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken, 
                include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK)
            );

            GetListResponse<GetListGuestAccommodationRoomListItemDto> response = _mapper.Map<GetListResponse<GetListGuestAccommodationRoomListItemDto>>(guestAccommodationRooms);
            return response;
        }
    }
}