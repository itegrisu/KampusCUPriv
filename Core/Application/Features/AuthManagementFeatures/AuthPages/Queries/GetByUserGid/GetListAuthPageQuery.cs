
using AutoMapper;
using MediatR;
using Core.Application.Responses;
using Core.Application.Request;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;

namespace Application.Features.AuthManagementFeatures.AuthPages.Queries.GetByUserGid;

public class GetByUserGidAuthPageQuery : IRequest<GetListResponse<GetByUserGidAuthPageListItemDto>>
{
    public Guid UserGid { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;

    public class GetByUserGidAuthPageQueryHandler : IRequestHandler<GetByUserGidAuthPageQuery, GetListResponse<GetByUserGidAuthPageListItemDto>>
    {
        private readonly IAuthRolePageReadRepository _authRolePageReadRepository;
        private readonly IAuthUserRoleReadRepository _authUserRoleReadRepository;
        private readonly IAuthPageReadRepository _authPageReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<AuthPage, GetByUserGidAuthPageListItemDto> _noPagination;
        public GetByUserGidAuthPageQueryHandler(IAuthPageReadRepository authPageReadRepository, IMapper mapper, NoPagination<AuthPage, GetByUserGidAuthPageListItemDto> noPagination, IAuthRolePageReadRepository authRolePageReadRepository, IAuthUserRoleReadRepository authUserRoleReadRepository)
        {
            _authPageReadRepository = authPageReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _authRolePageReadRepository = authRolePageReadRepository;
            _authUserRoleReadRepository = authUserRoleReadRepository;
        }

        public async Task<GetListResponse<GetByUserGidAuthPageListItemDto>> Handle(GetByUserGidAuthPageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AuthUserRole> roles;
            if (request.PageIndex == -1)
            {
                int size = _authUserRoleReadRepository.CountFull();

                roles = await _authUserRoleReadRepository.GetListAsync(
                              index: 0,
                              size: size,
                              include: source => source.Include(x => x.AuthRoleFK).ThenInclude(x => x.AuthRolePages).ThenInclude(x => x.AuthPageFK).Include(x => x.UserFK).Include(x => x.AuthPageFK),
                              predicate: x => x.UserFK.Gid == request.UserGid,
                               cancellationToken: cancellationToken);
            }
            else
            {
                roles = await _authUserRoleReadRepository.GetListAsync(
                              index: request.PageIndex,
                              size: request.PageSize,
                              include: source => source.Include(x => x.AuthRoleFK).ThenInclude(x => x.AuthRolePages).ThenInclude(x => x.AuthPageFK).Include(x => x.UserFK).Include(x => x.AuthPageFK),
                              predicate: x => x.UserFK.Gid == request.UserGid,
                               cancellationToken: cancellationToken);
            }



            List<AuthPage> pages = roles.Items
                            .Where(x => x.AuthPageFK != null) // Null kontrolü ekleniyor
                            .Select(x => x.AuthPageFK)
                            .Distinct()
                            .ToList();

            pages.AddRange(roles.Items.
                            Where(x => x.AuthRoleFK != null && x.DataState == Core.Enum.DataState.Active)
                           .SelectMany(x => x.AuthRoleFK.AuthRolePages).Where(x=>x.DataState==Core.Enum.DataState.Active)
                           .Select(x => x.AuthPageFK).Where(x=>x.DataState == Core.Enum.DataState.Active)
                           .Distinct().ToList());
            pages = pages.Distinct().OrderBy(x => x.RowNo).ToList();  


            return new GetListResponse<GetByUserGidAuthPageListItemDto>()
            {
                Size = request.PageSize,
                Index = request.PageIndex,
                Count = pages.Count,
                Items = _mapper.Map<List<GetByUserGidAuthPageListItemDto>>(pages),
                HasNext = roles.HasNext,
                HasPrevious = roles.HasPrevious,
            };

           
        }
    }
}