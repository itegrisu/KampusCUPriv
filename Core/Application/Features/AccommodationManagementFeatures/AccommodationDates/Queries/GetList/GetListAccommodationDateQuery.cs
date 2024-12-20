using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;

public class GetListAccommodationDateQuery : IRequest<GetListResponse<GetListAccommodationDateListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAccommodationDateQueryHandler : IRequestHandler<GetListAccommodationDateQuery, GetListResponse<GetListAccommodationDateListItemDto>>
    {
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.AccommodationDate, GetListAccommodationDateListItemDto> _noPagination;

        public GetListAccommodationDateQueryHandler(IAccommodationDateReadRepository accommodationDateReadRepository, IMapper mapper, NoPagination<X.AccommodationDate, GetListAccommodationDateListItemDto> noPagination)
        {
            _accommodationDateReadRepository = accommodationDateReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAccommodationDateListItemDto>> Handle(GetListAccommodationDateQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<AccommodationDate, object>>[]
                    {
                       x => x.GuestFK,
                       x=> x.ReservationDetailFK,
                       x=> x.ReservationRoomFK,
                    });
            IPaginate<X.AccommodationDate> accommodationDates = await _accommodationDateReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken, 
                include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK)
            );

            GetListResponse<GetListAccommodationDateListItemDto> response = _mapper.Map<GetListResponse<GetListAccommodationDateListItemDto>>(accommodationDates);
            return response;
        }
    }
}