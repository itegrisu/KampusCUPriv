using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetActiveUser
{
    public class GetActiveUserQuery : IRequest<GetListResponse<GetActiveUserListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public class GetActiveUserQueryHandler : IRequestHandler<GetActiveUserQuery, GetListResponse<GetActiveUserListItemDto>>
        {
            private readonly NoPagination<User, GetActiveUserListItemDto> _noPagination;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetActiveUserQueryHandler(NoPagination<User, GetActiveUserListItemDto> noPagination, IUserReadRepository userReadRepository, IMapper mapper)
            {
                _noPagination = noPagination;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetActiveUserListItemDto>> Handle(GetActiveUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationAllData(
                        cancellationToken,
                        predicate: x => x.IsActive == true,
                        includes: new Expression<Func<User, object>>[]
                        {
                         x => x.CountryFK,
                        });
                }

                IPaginate<User> userCustoms = await _userReadRepository.GetListAllAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.IsActive == true
                );

                GetListResponse<GetActiveUserListItemDto> response = _mapper.Map<GetListResponse<GetActiveUserListItemDto>>(userCustoms);
                return response;
            }
        }
    }

}
