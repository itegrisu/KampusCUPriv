using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetList;

public class GetListGuestAccommodationQuery : IRequest<GetListResponse<GetListGuestAccommodationListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGuestAccommodationQueryHandler : IRequestHandler<GetListGuestAccommodationQuery, GetListResponse<GetListGuestAccommodationListItemDto>>
    {
        private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.GuestAccommodation, GetListGuestAccommodationListItemDto> _noPagination;

        public GetListGuestAccommodationQueryHandler(IGuestAccommodationReadRepository guestAccommodationReadRepository, IMapper mapper, NoPagination<X.GuestAccommodation, GetListGuestAccommodationListItemDto> noPagination)
        {
            _guestAccommodationReadRepository = guestAccommodationReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListGuestAccommodationListItemDto>> Handle(GetListGuestAccommodationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<GuestAccommodation, object>>[]
                    {
                       x => x.SCCompanyFK,
                       x=> x.BuyCurrencyFK,
                       x=> x.SellCurrencyFK,
                    });
            IPaginate<X.GuestAccommodation> guestAccommodations = await _guestAccommodationReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.SCCompanyFK).Include(x => x.BuyCurrencyFK).Include(x => x.SellCurrencyFK)
            );

            GetListResponse<GetListGuestAccommodationListItemDto> response = _mapper.Map<GetListResponse<GetListGuestAccommodationListItemDto>>(guestAccommodations);
            return response;
        }
    }
}