using Application.Helpers.PaginationHelpers;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PortalManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetList;

public class GetListPortalTextQuery : IRequest<GetListResponse<GetListPortalTextListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPortalTextQueryHandler : IRequestHandler<GetListPortalTextQuery, GetListResponse<GetListPortalTextListItemDto>>
    {
        private readonly IPortalTextReadRepository _portalTextReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PortalText, GetListPortalTextListItemDto> _noPagination;

        public GetListPortalTextQueryHandler(IPortalTextReadRepository portalTextReadRepository, IMapper mapper, NoPagination<X.PortalText, GetListPortalTextListItemDto> noPagination)
        {
            _portalTextReadRepository = portalTextReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPortalTextListItemDto>> Handle(GetListPortalTextQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<PortalText, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.PortalTextMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.PortalText> portalTexts = await _portalTextReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPortalTextListItemDto> response = _mapper.Map<GetListResponse<GetListPortalTextListItemDto>>(portalTexts);
            return response;
        }
    }
}