using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetListDeleted
{
    public class GetListDeletedUserQuery : IRequest<GetListResponse<GetListDeletedUserListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListDeletedUserQueryHandler : IRequestHandler<GetListDeletedUserQuery, GetListResponse<GetListDeletedUserListItemDto>>
        {
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<User, GetListDeletedUserListItemDto> _noPagination;

            public GetListDeletedUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper, NoPagination<User, GetListDeletedUserListItemDto> noPagination)
            {
                _userReadRepository = userReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetListDeletedUserListItemDto>> Handle(GetListDeletedUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationAllData(
                        cancellationToken,
                        predicate: x => x.DataState == Core.Enum.DataState.Deleted,
                        includes: new Expression<Func<User, object>>[]
                        {
                        x=>x.CountryFK
                        });
                }

                IPaginate<User> users = await _userReadRepository.GetListAllAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.DataState == Core.Enum.DataState.Deleted,
                     include: i => i.Include(i => i.CountryFK)
                );


                GetListResponse<GetListDeletedUserListItemDto> response = _mapper.Map<GetListResponse<GetListDeletedUserListItemDto>>(users);
                return response;
            }
        }
    }
}
