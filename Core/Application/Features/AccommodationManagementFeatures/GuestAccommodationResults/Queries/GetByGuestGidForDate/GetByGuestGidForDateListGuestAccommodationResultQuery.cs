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

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGuestGidForDate
{
    public class GetByGuestGidForDateListGuestAccommodationResultQuery : IRequest<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GuestGid { get; set; }
        public class GetByGuestGidForDateListGuestAccommodationResultQueryHandler : IRequestHandler<GetByGuestGidForDateListGuestAccommodationResultQuery, GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>
        {
            private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationResult, GetByGuestGidForDateListGuestAccommodationResultListItemDto> _noPagination;

            public GetByGuestGidForDateListGuestAccommodationResultQueryHandler(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationResult, GetByGuestGidForDateListGuestAccommodationResultListItemDto> noPagination)
            {
                _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>> Handle(GetByGuestGidForDateListGuestAccommodationResultQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        includes: new Expression<Func<GuestAccommodationResult, object>>[]
                        {
                       x => x.GuestAccommodationPersonFK,
                       x=> x.GuestAccommodationRoomFK.RoomTypeFK,
                        });
                IPaginate<X.GuestAccommodationResult> guestAccommodationResults = await _guestAccommodationResultReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK)
                );

                GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto> response = _mapper.Map<GetListResponse<GetByGuestGidForDateListGuestAccommodationResultListItemDto>>(guestAccommodationResults);
                return response;
            }
        }
    }
}
