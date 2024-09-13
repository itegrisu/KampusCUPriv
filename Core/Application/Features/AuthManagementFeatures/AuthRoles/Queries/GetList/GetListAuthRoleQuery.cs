using AutoMapper;
using MediatR;
using Core.Application.Responses;
using Core.Application.Request;
using Core.Persistence.Paging;
using Domain.Entities.AuthManagements;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Queries.GetList;

public class GetListAuthRoleQuery : IRequest<GetListResponse<GetListAuthRoleListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAuthRoleQueryHandler : IRequestHandler<GetListAuthRoleQuery, GetListResponse<GetListAuthRoleListItemDto>>
    {
        private readonly IAuthRoleReadRepository _authRoleReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<AuthRole, GetListAuthRoleListItemDto> _noPagination;

        public GetListAuthRoleQueryHandler(IAuthRoleReadRepository authRoleRepository, IMapper mapper, NoPagination<AuthRole, GetListAuthRoleListItemDto> noPagination)
        {
            _authRoleReadRepository = authRoleRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAuthRoleListItemDto>> Handle(GetListAuthRoleQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationAllData(cancellationToken,
                    predicate: a => a.DataState == Core.Enum.DataState.Active || a.DataState == Core.Enum.DataState.Pasive, orderBy: a => a.RowNo);

            IPaginate<AuthRole> authRoles = await _authRoleReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                predicate: a => a.DataState == Core.Enum.DataState.Active || a.DataState == Core.Enum.DataState.Pasive,
                orderBy: o => o.OrderBy(o => o.RowNo),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAuthRoleListItemDto> response = _mapper.Map<GetListResponse<GetListAuthRoleListItemDto>>(authRoles);
            return response;
        }
    }
}