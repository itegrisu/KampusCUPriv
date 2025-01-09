using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationFileQuery : IRequest<GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid Gid { get; set; }
        public class GetByOrganizationGidListOrganizationFileQueryHandler : IRequestHandler<GetByOrganizationGidListOrganizationFileQuery, GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto>>
        {
            private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.OrganizationFile, GetByOrganizationGidListOrganizationFileListItemDto> _noPagination;

            public GetByOrganizationGidListOrganizationFileQueryHandler(IOrganizationFileReadRepository organizationFileReadRepository, IMapper mapper, NoPagination<X.OrganizationFile, GetByOrganizationGidListOrganizationFileListItemDto> noPagination)
            {
                _organizationFileReadRepository = organizationFileReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto>> Handle(GetByOrganizationGidListOrganizationFileQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        orderBy: x => x.RowNo,
                        predicate: x => x.GidOrganizationFK == request.Gid,
                        includes: new Expression<Func<OrganizationFile, object>>[]
                        {
                       x => x.OrganizationFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.OrganizationFile> organizationFiles = await _organizationFileReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    orderBy: x => x.OrderBy(x => x.RowNo),
                    include: x => x.Include(x => x.OrganizationFK)
                );

                GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto> response = _mapper.Map<GetListResponse<GetByOrganizationGidListOrganizationFileListItemDto>>(organizationFiles);
                return response;
            }
        }
    }
}
