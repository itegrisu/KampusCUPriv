using Application.Helpers.PaginationHelpers;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PortalManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.PortalManagementFeatures.PortalParameters.Queries.GetList;

public class GetListPortalParameterQuery : IRequest<GetListResponse<GetListPortalParameterListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPortalParameterQueryHandler : IRequestHandler<GetListPortalParameterQuery, GetListResponse<GetListPortalParameterListItemDto>>
    {
        private readonly IPortalParameterReadRepository _portalParameterReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PortalParameter, GetListPortalParameterListItemDto> _noPagination;

        public GetListPortalParameterQueryHandler(IPortalParameterReadRepository portalParameterReadRepository, IMapper mapper, NoPagination<X.PortalParameter, GetListPortalParameterListItemDto> noPagination)
        {
            _portalParameterReadRepository = portalParameterReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPortalParameterListItemDto>> Handle(GetListPortalParameterQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PortalParameter, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PortalParameterMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PortalParameter> portalParameters = await _portalParameterReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPortalParameterListItemDto> response = _mapper.Map<GetListResponse<GetListPortalParameterListItemDto>>(portalParameters);
            return response;
        }
    }
}