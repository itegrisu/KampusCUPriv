using Application.Helpers.PaginationHelpers;
using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.OrganizationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.OrganizationManagements;

namespace Application.Features.OrganizationManagementFeatures.OrganizationItemMessages.Queries.GetList;

public class GetListOrganizationItemMessageQuery : IRequest<GetListResponse<GetListOrganizationItemMessageListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid GidOrganizationItemFK { get; set; }

    public class GetListOrganizationItemMessageQueryHandler : IRequestHandler<GetListOrganizationItemMessageQuery, GetListResponse<GetListOrganizationItemMessageListItemDto>>
    {
        private readonly IOrganizationItemMessageReadRepository _organizationItemMessageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.OrganizationItemMessage, GetListOrganizationItemMessageListItemDto> _noPagination;

        public GetListOrganizationItemMessageQueryHandler(IOrganizationItemMessageReadRepository organizationItemMessageReadRepository, IMapper mapper, NoPagination<X.OrganizationItemMessage, GetListOrganizationItemMessageListItemDto> noPagination)
        {
            _organizationItemMessageReadRepository = organizationItemMessageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListOrganizationItemMessageListItemDto>> Handle(GetListOrganizationItemMessageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    predicate: x => x.GidOrganizationItemFK == request.GidOrganizationItemFK,
                    includes: new Expression<Func<OrganizationItemMessage, object>>[]
                    {
                       x => x.UserFK,
                       x=> x.OrganizationItemFK
                    });

            IPaginate<X.OrganizationItemMessage> organizationItemMessages = await _organizationItemMessageReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                predicate: x => x.GidOrganizationItemFK == request.GidOrganizationItemFK,
                include: i => i.Include(i => i.UserFK).Include(i => i.OrganizationItemFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrganizationItemMessageListItemDto> response = _mapper.Map<GetListResponse<GetListOrganizationItemMessageListItemDto>>(organizationItemMessages);
            return response;
        }
    }
}