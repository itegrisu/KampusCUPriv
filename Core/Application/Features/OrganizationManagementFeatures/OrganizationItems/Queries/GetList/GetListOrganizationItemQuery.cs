using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItems.Queries.GetList;

public class GetListOrganizationItemQuery : IRequest<GetListResponse<GetListOrganizationItemListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationItemQueryHandler : IRequestHandler<GetListOrganizationItemQuery, GetListResponse<GetListOrganizationItemListItemDto>>
    {
        private readonly IOrganizationItemReadRepository _organizationItemReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationItem, GetListOrganizationItemListItemDto> _noPagination;

        public GetListOrganizationItemQueryHandler(IOrganizationItemReadRepository organizationItemReadRepository, IMapper mapper, NoPagination<X.OrganizationItem, GetListOrganizationItemListItemDto> noPagination)
        {
            _organizationItemReadRepository = organizationItemReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationItemListItemDto>> Handle(GetListOrganizationItemQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<OrganizationItem, object>>[]
                   {
                       x=>x.OrganizationGroupFK,
                       x=>x.MainResponsibleUserFK
                   });
            }


            IPaginate<X.OrganizationItem> organizationItems = await _organizationItemReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.OrganizationGroupFK).Include(x => x.MainResponsibleUserFK)
            );

            GetListResponse<GetListOrganizationItemListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationItemListItemDto>>(organizationItems);
            return response;
        }
    }
}