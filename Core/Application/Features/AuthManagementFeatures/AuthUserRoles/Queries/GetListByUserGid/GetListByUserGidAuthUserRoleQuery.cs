using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetListByUserGid;

public class GetListByUserGidAuthUserRoleQuery : IRequest<GetListResponse<GetListByUserGidAuthUserRoleListItemDto>>
{
    public Guid UserGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetListAuthUserRoleQueryHandler : IRequestHandler<GetListByUserGidAuthUserRoleQuery, GetListResponse<GetListByUserGidAuthUserRoleListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        private readonly NoPagination<AuthUserRole, GetListByUserGidAuthUserRoleListItemDto> _noPagination;
        public GetListAuthUserRoleQueryHandler(IMapper mapper, IAuthUserRoleReadRepository authUserRoleReadRepository, NoPagination<AuthUserRole, GetListByUserGidAuthUserRoleListItemDto> noPagination)
        {
            _mapper = mapper;
            _authUserRoleReadRepository = authUserRoleReadRepository;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListByUserGidAuthUserRoleListItemDto>> Handle(GetListByUserGidAuthUserRoleQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<AuthUserRole, object>>[]
                   {
                       x => x.AuthPageFK,
                       x => x.AuthRoleFK,
                       x => x.UserFK
                   },
                   predicate: x => x.GidUserFK == request.UserGid,
                   orderBy: a => a.RowNo);
            }


            IPaginate<AuthUserRole> authUserRoles = await _authUserRoleReadRepository.GetListAsync(
                index: request.PageIndex,
                size: request.PageSize,
                include: aur => aur.Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK).Include(i => i.UserFK),
                predicate: x => x.GidUserFK == request.UserGid,
                orderBy: o => o.OrderBy(o => o.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByUserGidAuthUserRoleListItemDto> response = _mapper.Map<GetListResponse<GetListByUserGidAuthUserRoleListItemDto>>(authUserRoles);
            return response;
        }
    }
}