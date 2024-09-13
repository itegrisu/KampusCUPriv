using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Queries.GetList;

public class GetListAuthRolePageQuery : IRequest<GetListResponse<GetListAuthRolePageListItemDto>>
{
    public Guid RoleGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetListAuthRolePageQueryHandler : IRequestHandler<GetListAuthRolePageQuery, GetListResponse<GetListAuthRolePageListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly NoPagination<AuthRolePage, GetListAuthRolePageListItemDto> _noPagination;

        public GetListAuthRolePageQueryHandler(IAuthRolePageReadRepository authRolePageReadRepository, IMapper mapper, NoPagination<AuthRolePage, GetListAuthRolePageListItemDto> noPagination)
        {
            _authRolePageReadRepository = authRolePageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAuthRolePageListItemDto>> Handle(GetListAuthRolePageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<AuthRolePage, object>>[]
                           {
                                x => x.AuthPageFK,
                                x => x.AuthRoleFK
                           },
                    predicate: x => x.GidRoleFK == request.RoleGid,
                    orderBy: x => x.RowNo);
            }


            IPaginate<AuthRolePage> authRolePages = await _authRolePageReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                include: arp => arp.Include(arp => arp.AuthRoleFK).Include(arp => arp.AuthPageFK),
                predicate:x=>x.GidRoleFK == request.RoleGid,
                orderBy: o => o.OrderBy(o => o.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAuthRolePageListItemDto> response = _mapper.Map<GetListResponse<GetListAuthRolePageListItemDto>>(authRolePages);
            return response;
        }
    }
}