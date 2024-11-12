using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemFiles.Queries.GetList;

public class GetListOrganizationItemFileQuery : IRequest<GetListResponse<GetListOrganizationItemFileListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationItemFileQueryHandler : IRequestHandler<GetListOrganizationItemFileQuery, GetListResponse<GetListOrganizationItemFileListItemDto>>
    {
        private readonly IOrganizationItemFileReadRepository _organizationItemFileReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationItemFile, GetListOrganizationItemFileListItemDto> _noPagination;

        public GetListOrganizationItemFileQueryHandler(IOrganizationItemFileReadRepository organizationItemFileReadRepository, IMapper mapper, NoPagination<X.OrganizationItemFile, GetListOrganizationItemFileListItemDto> noPagination)
        {
            _organizationItemFileReadRepository = organizationItemFileReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationItemFileListItemDto>> Handle(GetListOrganizationItemFileQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<OrganizationItemFile, object>>[]
                    {
                       x => x.OrganizationItemFK
                    });

            IPaginate<X.OrganizationItemFile> organizationItemFiles = await _organizationItemFileReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: i => i.Include(i => i.OrganizationItemFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrganizationItemFileListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationItemFileListItemDto>>(organizationItemFiles);
            return response;
        }
    }
}