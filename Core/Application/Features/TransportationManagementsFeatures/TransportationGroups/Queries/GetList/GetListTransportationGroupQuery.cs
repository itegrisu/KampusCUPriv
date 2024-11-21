using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetList;

public class GetListTransportationGroupQuery : IRequest<GetListResponse<GetListTransportationGroupListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationGroupQueryHandler : IRequestHandler<GetListTransportationGroupQuery, GetListResponse<GetListTransportationGroupListItemDto>>
    {
        private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TransportationGroup, GetListTransportationGroupListItemDto> _noPagination;

        public GetListTransportationGroupQueryHandler(ITransportationGroupReadRepository transportationGroupReadRepository, IMapper mapper, NoPagination<X.TransportationGroup, GetListTransportationGroupListItemDto> noPagination)
        {
            _transportationGroupReadRepository = transportationGroupReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationGroupListItemDto>> Handle(GetListTransportationGroupQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TransportationGroup, object>>[]
                    {
                       x => x.StartCountryFK,
                       x => x.StartCityFK,
                       x => x.StartDistrictFK,
                       x => x.EndCountryFK,
                       x => x.EndCityFK,
                       x => x.EndDistrictFK,
                       x => x.TransportationServiceFK
                    });

            IPaginate<X.TransportationGroup> transportationGroups = await _transportationGroupReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK)
            );

            GetListResponse<GetListTransportationGroupListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationGroupListItemDto>>(transportationGroups);
            return response;
        }
    }
}