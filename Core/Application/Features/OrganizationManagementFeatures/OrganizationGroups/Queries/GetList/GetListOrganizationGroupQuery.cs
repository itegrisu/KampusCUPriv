using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetList;

public class GetListOrganizationGroupQuery : IRequest<GetListResponse<GetListOrganizationGroupListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationGroupQueryHandler : IRequestHandler<GetListOrganizationGroupQuery, GetListResponse<GetListOrganizationGroupListItemDto>>
    {
        private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationGroup, GetListOrganizationGroupListItemDto> _noPagination;

        public GetListOrganizationGroupQueryHandler(IOrganizationGroupReadRepository organizationGroupReadRepository, IMapper mapper, NoPagination<X.OrganizationGroup, GetListOrganizationGroupListItemDto> noPagination)
        {
            _organizationGroupReadRepository = organizationGroupReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationGroupListItemDto>> Handle(GetListOrganizationGroupQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    orderBy: x => x.RowNo,
                    includes: new Expression<Func<OrganizationGroup, object>>[]
                    {
                      x=>x.OrganizationFK
                    });

            }

            IPaginate<X.OrganizationGroup> organizationGroups = await _organizationGroupReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(x => x.RowNo),
                include: x => x.Include(x => x.OrganizationFK)
            );

            GetListResponse<GetListOrganizationGroupListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationGroupListItemDto>>(organizationGroups);
            return response;
        }
    }
}