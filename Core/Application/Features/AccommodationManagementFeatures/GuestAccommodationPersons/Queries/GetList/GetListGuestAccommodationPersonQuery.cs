using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetList;

public class GetListGuestAccommodationPersonQuery : IRequest<GetListResponse<GetListGuestAccommodationPersonListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGuestAccommodationPersonQueryHandler : IRequestHandler<GetListGuestAccommodationPersonQuery, GetListResponse<GetListGuestAccommodationPersonListItemDto>>
    {
        private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.GuestAccommodationPerson, GetListGuestAccommodationPersonListItemDto> _noPagination;

        public GetListGuestAccommodationPersonQueryHandler(IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationPerson, GetListGuestAccommodationPersonListItemDto> noPagination)
        {
            _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListGuestAccommodationPersonListItemDto>> Handle(GetListGuestAccommodationPersonQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<GuestAccommodationPerson, object>>[]
                    {
                       x => x.GuestAccommodationFK,
                       x=> x.CountryFK
                    });
            IPaginate<X.GuestAccommodationPerson> guestAccommodationPersons = await _guestAccommodationPersonReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.CountryFK)
            );

            GetListResponse<GetListGuestAccommodationPersonListItemDto> response = _mapper.Map<GetListResponse<GetListGuestAccommodationPersonListItemDto>>(guestAccommodationPersons);
            return response;
        }
    }
}