using Application.Helpers.PaginationHelpers;
using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.DefinitionManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetList;

public class GetListOrganizationTypeQuery : IRequest<GetListResponse<GetListOrganizationTypeListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationTypeQueryHandler : IRequestHandler<GetListOrganizationTypeQuery, GetListResponse<GetListOrganizationTypeListItemDto>>
    {
        private readonly IOrganizationTypeReadRepository _organizationTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationType, GetListOrganizationTypeListItemDto> _noPagination;

        public GetListOrganizationTypeQueryHandler(IOrganizationTypeReadRepository organizationTypeReadRepository, IMapper mapper, NoPagination<X.OrganizationType, GetListOrganizationTypeListItemDto> noPagination)
        {
            _organizationTypeReadRepository = organizationTypeReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationTypeListItemDto>> Handle(GetListOrganizationTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<OrganizationType, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.OrganizationTypeMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.OrganizationType> organizationTypes = await _organizationTypeReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrganizationTypeListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationTypeListItemDto>>(organizationTypes);
            return response;
        }
    }
}