using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationItemFileQuery : IRequest<GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidOrganizationItemFK { get; set; }

        public class GetByOrganizationGidListOrganizationItemFileQueryHandler : IRequestHandler<GetByOrganizationGidListOrganizationItemFileQuery, GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto>>
        {
            private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<OrganizationItemFile, GetByOrganizationGidListOrganizationItemFileListItemDto> _noPagination;

            public GetByOrganizationGidListOrganizationItemFileQueryHandler(IOrganizationItemFileReadRepository organizationItemFileReadRepository, IMapper mapper, NoPagination<OrganizationItemFile, GetByOrganizationGidListOrganizationItemFileListItemDto> noPagination)
            {
                _organizationItemFileReadRepository = organizationItemFileReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto>> Handle(GetByOrganizationGidListOrganizationItemFileQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidOrganizationItemFK == request.GidOrganizationItemFK,
                        orderBy: x => x.RowNo,
                        includes: new Expression<Func<OrganizationItemFile, object>>[]
                        {
                       x => x.OrganizationItemFK
                        });

                IPaginate<OrganizationItemFile> organizationItemFiles = await _organizationItemFileReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    include: i => i.Include(i => i.OrganizationItemFK),
                    orderBy: x => x.OrderBy(x => x.RowNo),
                    predicate: x => x.GidOrganizationItemFK == request.GidOrganizationItemFK,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto> response = _mapper.Map<GetListResponse<GetByOrganizationGidListOrganizationItemFileListItemDto>>(organizationItemFiles);
                return response;
            }
        }

    }
}
