using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetList;

public class GetListOrganizationFileQuery : IRequest<GetListResponse<GetListOrganizationFileListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationFileQueryHandler : IRequestHandler<GetListOrganizationFileQuery, GetListResponse<GetListOrganizationFileListItemDto>>
    {
        private readonly IOrganizationFileReadRepository _organizationFileReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationFile, GetListOrganizationFileListItemDto> _noPagination;

        public GetListOrganizationFileQueryHandler(IOrganizationFileReadRepository organizationFileReadRepository, IMapper mapper, NoPagination<X.OrganizationFile, GetListOrganizationFileListItemDto> noPagination)
        {
            _organizationFileReadRepository = organizationFileReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationFileListItemDto>> Handle(GetListOrganizationFileQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    orderBy: x => x.RowNo,
                    includes: new Expression<Func<OrganizationFile, object>>[]
                    {
                       x => x.OrganizationFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.OrganizationFile> organizationFiles = await _organizationFileReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderBy(x => x.RowNo),
                include: x => x.Include(x => x.OrganizationFK)
            );

            GetListResponse<GetListOrganizationFileListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationFileListItemDto>>(organizationFiles);
            return response;
        }
    }
}