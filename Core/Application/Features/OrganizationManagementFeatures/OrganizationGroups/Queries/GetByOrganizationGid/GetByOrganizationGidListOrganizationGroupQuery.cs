using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationGroupQuery : IRequest<GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid OrganizationGid { get; set; }
        public class GetByOrganizationGidListOrganizationGroupQueryHandler : IRequestHandler<GetByOrganizationGidListOrganizationGroupQuery, GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto>>
        {
            private readonly IOrganizationGroupReadRepository _organizationGroupReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.OrganizationGroup, GetByOrganizationGidListOrganizationGroupListItemDto> _noPagination;

            public GetByOrganizationGidListOrganizationGroupQueryHandler(IOrganizationGroupReadRepository organizationGroupReadRepository, IMapper mapper, NoPagination<X.OrganizationGroup, GetByOrganizationGidListOrganizationGroupListItemDto> noPagination)
            {
                _organizationGroupReadRepository = organizationGroupReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto>> Handle(GetByOrganizationGidListOrganizationGroupQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidOrganizationFK == request.OrganizationGid,
                         orderBy: x => x.RowNo,
                        includes: new Expression<Func<OrganizationGroup, object>>[]
                        {
                            x=>x.OrganizationFK
                        });

                }

                IPaginate<X.OrganizationGroup> organizationGroups = await _organizationGroupReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidOrganizationFK == request.OrganizationGid,
                     orderBy: x => x.OrderBy(x => x.RowNo),
                    include: x => x.Include(x => x.OrganizationFK)
                );

                GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto> response = _mapper.Map<GetListResponse<GetByOrganizationGidListOrganizationGroupListItemDto>>(organizationGroups);
                return response;
            }
        }
    }
}
