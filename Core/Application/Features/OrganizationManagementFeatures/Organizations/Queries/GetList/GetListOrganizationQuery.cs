using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.Organizations.Queries.GetList;

public class GetListOrganizationQuery : IRequest<GetListResponse<GetListOrganizationListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOrganizationQueryHandler : IRequestHandler<GetListOrganizationQuery, GetListResponse<GetListOrganizationListItemDto>>
    {
        private readonly IOrganizationReadRepository _organizationReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.Organization, GetListOrganizationListItemDto> _noPagination;

        public GetListOrganizationQueryHandler(IOrganizationReadRepository organizationReadRepository, IMapper mapper, NoPagination<X.Organization, GetListOrganizationListItemDto> noPagination)
        {
            _organizationReadRepository = organizationReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationListItemDto>> Handle(GetListOrganizationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<Organization, object>>[]
                    {
                       x=>x.SCCompanyFK,
                       x=>x.OrganizationTypeFK,
                       x=>x.ResponsibleUserFK

                    });

            }

            IPaginate<X.Organization> organizations = await _organizationReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: X => X.Include(x => x.SCCompanyFK).Include(x => x.OrganizationTypeFK).Include(x => x.ResponsibleUserFK)
            );

            GetListResponse<GetListOrganizationListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationListItemDto>>(organizations);
            return response;
        }
    }
}