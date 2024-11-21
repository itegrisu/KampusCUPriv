using Application.Helpers.PaginationHelpers;
using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetList;

public class GetListTransportationExternalServiceQuery : IRequest<GetListResponse<GetListTransportationExternalServiceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransportationExternalServiceQueryHandler : IRequestHandler<GetListTransportationExternalServiceQuery, GetListResponse<GetListTransportationExternalServiceListItemDto>>
    {
        private readonly ITransportationExternalServiceReadRepository _transportationExternalServiceReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.TransportationExternalService, GetListTransportationExternalServiceListItemDto> _noPagination;

        public GetListTransportationExternalServiceQueryHandler(ITransportationExternalServiceReadRepository transportationExternalServiceReadRepository, IMapper mapper, NoPagination<X.TransportationExternalService, GetListTransportationExternalServiceListItemDto> noPagination)
        {
            _transportationExternalServiceReadRepository = transportationExternalServiceReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListTransportationExternalServiceListItemDto>> Handle(GetListTransportationExternalServiceQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<TransportationExternalService, object>>[]
                    {
                       x => x.FeeCurrencyFK,
                       x => x.OrganizationFK,
                       x => x.SCCompanyFK,
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.TransportationExternalService> transportationExternalServices = await _transportationExternalServiceReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.FeeCurrencyFK).Include(x => x.OrganizationFK).Include(x => x.SCCompanyFK)
            );

            GetListResponse<GetListTransportationExternalServiceListItemDto> response = _mapper.Map<GetListResponse<GetListTransportationExternalServiceListItemDto>>(transportationExternalServices);
            return response;
        }
    }
}