using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetList;

public class GetListTransportationServiceQuery : IRequest<GetListResponse<GetListTransportationServiceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationServiceQueryHandler : IRequestHandler<GetListTransportationServiceQuery, GetListResponse<GetListTransportationServiceListItemDto>>
    {
        private readonly ITransportationServiceReadRepository _transportationServiceReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TransportationService, GetListTransportationServiceListItemDto> _noPagination;

        public GetListTransportationServiceQueryHandler(ITransportationServiceReadRepository transportationServiceReadRepository, IMapper mapper, NoPagination<X.TransportationService, GetListTransportationServiceListItemDto> noPagination)
        {
            _transportationServiceReadRepository = transportationServiceReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationServiceListItemDto>> Handle(GetListTransportationServiceQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)

                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TransportationService, object>>[]
                    {
                       x => x.TransportationFK,
                       x=> x.VehicleAllFK
                    });
            IPaginate<X.TransportationService> transportationServices = await _transportationServiceReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.TransportationFK).Include(x => x.VehicleAllFK)
            );

            GetListResponse<GetListTransportationServiceListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationServiceListItemDto>>(transportationServices);
            return response;
        }
    }
}