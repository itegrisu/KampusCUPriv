using Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGuestGid
{
    public class GetByGuestGidListGuestAccommodationRoomQuery : IRequest<GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GuestGid { get; set; }
        public class GetByGuestGidListGuestAccommodationRoomQueryHandler : IRequestHandler<GetByGuestGidListGuestAccommodationRoomQuery, GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto>>
        {
            private readonly IGuestAccommodationRoomReadRepository _guestAccommodationRoomReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationRoom, GetByGuestGidListGuestAccommodationRoomListItemDto> _noPagination;

            public GetByGuestGidListGuestAccommodationRoomQueryHandler(IGuestAccommodationRoomReadRepository guestAccommodationRoomReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationRoom, GetByGuestGidListGuestAccommodationRoomListItemDto> noPagination)
            {
                _guestAccommodationRoomReadRepository = guestAccommodationRoomReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto>> Handle(GetByGuestGidListGuestAccommodationRoomQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidGuestAccommodationFK == request.GuestGid,
                        includes: new Expression<Func<GuestAccommodationRoom, object>>[]
                        {
                           x => x.GuestAccommodationFK,
                           x => x.RoomTypeFK,
                           x => x.GuestAccommodationResults
                        });
                IPaginate<X.GuestAccommodationRoom> guestAccommodationRooms = await _guestAccommodationRoomReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.RoomTypeFK).Include(x => x.GuestAccommodationResults),
                    predicate: x => x.GidGuestAccommodationFK == request.GuestGid
                );

                GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto> response = _mapper.Map<GetListResponse<GetByGuestGidListGuestAccommodationRoomListItemDto>>(guestAccommodationRooms);
                return response;
            }
        }
    }
}
