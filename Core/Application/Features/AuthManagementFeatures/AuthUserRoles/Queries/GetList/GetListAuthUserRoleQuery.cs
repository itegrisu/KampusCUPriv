using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetList
{
    public class GetListAuthUserRoleQuery : IRequest<GetListResponse<GetListAuthUserRoleListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListAuthUserRoleQueryHandler : IRequestHandler<GetListAuthUserRoleQuery, GetListResponse<GetListAuthUserRoleListItemDto>>
        {
            private readonly IMapper _mapper;
            private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
            private readonly NoPagination<AuthUserRole, GetListAuthUserRoleListItemDto> _noPagination;
            public GetListAuthUserRoleQueryHandler(IMapper mapper, IAuthUserRoleReadRepository authUserRoleReadRepository, NoPagination<AuthUserRole, GetListAuthUserRoleListItemDto> noPagination)
            {
                _mapper = mapper;
                _authUserRoleReadRepository = authUserRoleReadRepository;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListAuthUserRoleListItemDto>> Handle(GetListAuthUserRoleQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       includes: new Expression<Func<AuthUserRole, object>>[]
                       {
                       x => x.AuthPageFK,
                       x => x.AuthRoleFK,
                       x => x.UserFK
                       },
                       orderBy: a => a.RowNo);
                }


                IPaginate<AuthUserRole> authUserRoles = await _authUserRoleReadRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: aur => aur.Include(i => i.AuthRoleFK).Include(i => i.AuthPageFK).Include(i => i.UserFK),
                    orderBy: o => o.OrderBy(o => o.RowNo),
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListAuthUserRoleListItemDto> response = _mapper.Map<GetListResponse<GetListAuthUserRoleListItemDto>>(authUserRoles);
                return response;
            }
        }
    }
}
