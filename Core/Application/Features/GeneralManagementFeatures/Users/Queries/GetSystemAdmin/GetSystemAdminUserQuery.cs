using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetSystemAdmin
{
    public class GetSystemAdminUserQuery : IRequest<GetListResponse<GetSystemAdminUserListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public class GetSystemAdminUserQueryHandler : IRequestHandler<GetSystemAdminUserQuery, GetListResponse<GetSystemAdminUserListItemDto>>
        {
            private readonly NoPagination<User, GetSystemAdminUserListItemDto> _noPagination;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetSystemAdminUserQueryHandler(NoPagination<User, GetSystemAdminUserListItemDto> noPagination, IUserReadRepository userReadRepository, IMapper mapper)
            {
                _noPagination = noPagination;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetSystemAdminUserListItemDto>> Handle(GetSystemAdminUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationAllData(
                        cancellationToken,
                        predicate: u => u.IsSystemAdmin == true || u.DataState == Core.Enum.DataState.None,
                        includes: new Expression<Func<User, object>>[]
                        {
                         x => x.CountryFK,
                        });
                }

                IPaginate<User> userCustoms = await _userReadRepository.GetListAllAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: u => u.IsSystemAdmin == true || u.DataState == Core.Enum.DataState.None

                );

                GetListResponse<GetSystemAdminUserListItemDto> response = _mapper.Map<GetListResponse<GetSystemAdminUserListItemDto>>(userCustoms);
                return response;
            }
        }
    }
}