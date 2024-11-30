using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Queries.GetByServiceGid
{
    public class GetByServiceGidListTransportationGroupQuery : IRequest<GetListResponse<GetByServiceGidListTransportationGroupListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid ServiceGid { get; set; }
        public class GetByServiceGidListTransportationGroupQueryHandler : IRequestHandler<GetByServiceGidListTransportationGroupQuery, GetListResponse<GetByServiceGidListTransportationGroupListItemDto>>
        {
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.TransportationGroup, GetByServiceGidListTransportationGroupListItemDto> _noPagination;

            public GetByServiceGidListTransportationGroupQueryHandler(ITransportationGroupReadRepository transportationGroupReadRepository, IMapper mapper, NoPagination<X.TransportationGroup, GetByServiceGidListTransportationGroupListItemDto> noPagination)
            {
                _transportationGroupReadRepository = transportationGroupReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByServiceGidListTransportationGroupListItemDto>> Handle(GetByServiceGidListTransportationGroupQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTransportationServiceFK == request.ServiceGid,
                        includes: new Expression<Func<TransportationGroup, object>>[]
                        {
                           x => x.StartCountryFK,
                           x => x.StartCityFK,
                           x => x.StartDistrictFK,
                           x => x.EndCountryFK,
                           x => x.EndCityFK,
                           x => x.EndDistrictFK,
                           x => x.TransportationServiceFK,
                           x => x.TransportationPassengers
                        });

                IPaginate<X.TransportationGroup> transportationGroups = await _transportationGroupReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidTransportationServiceFK == request.ServiceGid,
                     include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK).Include(x => x.TransportationPassengers)
                );

                GetListResponse<GetByServiceGidListTransportationGroupListItemDto> response = _mapper.Map<GetListResponse<GetByServiceGidListTransportationGroupListItemDto>>(transportationGroups);
                return response;
            }
        }
    }
}
