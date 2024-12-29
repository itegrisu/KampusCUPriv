using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetList;

public class GetListGuestAccommodationResultQuery : IRequest<GetListResponse<GetListGuestAccommodationResultListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGuestAccommodationResultQueryHandler : IRequestHandler<GetListGuestAccommodationResultQuery, GetListResponse<GetListGuestAccommodationResultListItemDto>>
    {
        private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.GuestAccommodationResult, GetListGuestAccommodationResultListItemDto> _noPagination;

        public GetListGuestAccommodationResultQueryHandler(IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationResult, GetListGuestAccommodationResultListItemDto> noPagination)
        {
            _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListGuestAccommodationResultListItemDto>> Handle(GetListGuestAccommodationResultQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<GuestAccommodationResult, object>>[]
                    {
                       x => x.GuestAccommodationPersonFK,
                       x=> x.GuestAccommodationRoomFK.RoomTypeFK,
                    });
            IPaginate<X.GuestAccommodationResult> guestAccommodationResults = await _guestAccommodationResultReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK)
            );

            GetListResponse<GetListGuestAccommodationResultListItemDto> response = _mapper.Map<GetListResponse<GetListGuestAccommodationResultListItemDto>>(guestAccommodationResults);
            return response;
        }
    }
}