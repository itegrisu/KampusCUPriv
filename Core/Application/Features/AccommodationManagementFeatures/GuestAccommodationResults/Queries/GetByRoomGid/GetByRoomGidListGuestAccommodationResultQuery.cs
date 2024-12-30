using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
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

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGid
{
    public class GetByRoomGidListGuestAccommodationResultQuery : IRequest<GetListResponse<GetByRoomGidListGuestAccommodationResultListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid RoomGid { get; set; }
        public class GetByRoomGidListGuestAccommodationResultQueryQueryHandler : IRequestHandler<GetByRoomGidListGuestAccommodationResultQuery, GetListResponse<GetByRoomGidListGuestAccommodationResultListItemDto>>
        {
            private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationResult, GetByRoomGidListGuestAccommodationResultListItemDto> _noPagination;

            public GetByRoomGidListGuestAccommodationResultQueryQueryHandler(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationResult, GetByRoomGidListGuestAccommodationResultListItemDto> noPagination)
            {
                _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByRoomGidListGuestAccommodationResultListItemDto>> Handle(GetByRoomGidListGuestAccommodationResultQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidGuestAccommodationRoomFK == request.RoomGid,
                        includes: new Expression<Func<GuestAccommodationResult, object>>[]
                        {
                       x => x.GuestAccommodationPersonFK,
                       x=> x.GuestAccommodationRoomFK.RoomTypeFK,
                        });
                IPaginate<X.GuestAccommodationResult> guestAccommodationResults = await _guestAccommodationResultReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidGuestAccommodationRoomFK == request.RoomGid,
                    include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK)
                );

                GetListResponse<GetByRoomGidListGuestAccommodationResultListItemDto> response = _mapper.Map<GetListResponse<GetByRoomGidListGuestAccommodationResultListItemDto>>(guestAccommodationResults);
                return response;
            }
        }
    }
}
