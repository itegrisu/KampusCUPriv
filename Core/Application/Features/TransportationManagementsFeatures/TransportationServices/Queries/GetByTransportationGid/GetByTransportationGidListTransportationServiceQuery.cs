using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
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

namespace Application.Features.TransportationManagementsFeatures.TransportationServices.Queries.GetByTransportationGid
{
    public class GetByTransportationGidListTransportationServiceQuery : IRequest<GetListResponse<GetListTransportationServiceListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid TransportationGid { get; set; }

        public class GetByTransportationGidListTransportationServiceQueryHandler : IRequestHandler<GetByTransportationGidListTransportationServiceQuery, GetListResponse<GetListTransportationServiceListItemDto>>
        {
            private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.TransportationService, GetListTransportationServiceListItemDto> _noPagination;

            public GetByTransportationGidListTransportationServiceQueryHandler(ITransportationServiceReadRepository transportationServiceReadRepository, IMapper mapper, NoPagination<X.TransportationService, GetListTransportationServiceListItemDto> noPagination)
            {
                _transportationServiceReadRepository = transportationServiceReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListTransportationServiceListItemDto>> Handle(GetByTransportationGidListTransportationServiceQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)

                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidTransportationFK == request.TransportationGid,
                        includes: new Expression<Func<TransportationService, object>>[]
                        {
                       x => x.TransportationFK,
                       x=> x.VehicleAllFK
                        });
                IPaginate<X.TransportationService> transportationServices = await _transportationServiceReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidTransportationFK == request.TransportationGid,
                    include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK)
                );

                GetListResponse<GetListTransportationServiceListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationServiceListItemDto>>(transportationServices);
                return response;
            }
        }
    }
}
