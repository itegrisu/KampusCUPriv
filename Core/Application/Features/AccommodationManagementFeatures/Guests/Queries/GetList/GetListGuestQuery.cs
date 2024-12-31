using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.GuestRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.Guests.Queries.GetList;

public class GetListGuestQuery : IRequest<GetListResponse<GetListGuestListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGuestQueryHandler : IRequestHandler<GetListGuestQuery, GetListResponse<GetListGuestListItemDto>>
    {
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Guest, GetListGuestListItemDto> _noPagination;

        public GetListGuestQueryHandler(IGuestReadRepository guestReadRepository, IMapper mapper, NoPagination<X.Guest, GetListGuestListItemDto> noPagination)
        {
            _guestReadRepository = guestReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListGuestListItemDto>> Handle(GetListGuestQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Guest, object>>[]
                    {
                       x => x.CountryFK,
                    });
            IPaginate<X.Guest> guests = await _guestReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.CountryFK),
                orderBy: x => x.OrderByDescending(x => x.CreatedDate)
            );

            GetListResponse<GetListGuestListItemDto> response = _mapper.Map<GetListResponse<GetListGuestListItemDto>>(guests);
            return response;
        }
    }
}