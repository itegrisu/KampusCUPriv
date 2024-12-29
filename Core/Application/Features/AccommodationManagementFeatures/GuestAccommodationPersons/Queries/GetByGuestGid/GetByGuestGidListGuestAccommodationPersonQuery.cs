using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
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

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGuestGid
{
    public class GetByGuestGidListGuestAccommodationPersonQuery : IRequest<GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GuestGid { get; set; }
        public class GetByGuestGidListGuestAccommodationPersonQueryHandler : IRequestHandler<GetByGuestGidListGuestAccommodationPersonQuery, GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto>>
        {
            private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.GuestAccommodationPerson, GetByGuestGidListGuestAccommodationPersonListItemDto> _noPagination;

            public GetByGuestGidListGuestAccommodationPersonQueryHandler(IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository, IMapper mapper, NoPagination<X.GuestAccommodationPerson, GetByGuestGidListGuestAccommodationPersonListItemDto> noPagination)
            {
                _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto>> Handle(GetByGuestGidListGuestAccommodationPersonQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidGuestAccommodationFK == request.GuestGid,
                        includes: new Expression<Func<GuestAccommodationPerson, object>>[]
                        {
                       x => x.GuestAccommodationFK,
                       x=> x.CountryFK
                        });
                IPaginate<X.GuestAccommodationPerson> guestAccommodationPersons = await _guestAccommodationPersonReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.CountryFK),
                    predicate: x => x.GidGuestAccommodationFK == request.GuestGid
                );

                GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto> response = _mapper.Map<GetListResponse<GetByGuestGidListGuestAccommodationPersonListItemDto>>(guestAccommodationPersons);
                return response;
            }
        }
    }
}
