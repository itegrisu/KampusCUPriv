using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.GeneralManagementRepo.UserRepo;

namespace Application.Features.GeneralFeatures.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.User, GetListUserListItemDto> _noPagination;

        public GetListUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper, NoPagination<X.User, GetListUserListItemDto> noPagination)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
				//includes varsa eklenecek - Orn: Altta
				//return await _noPagination.NoPaginationData(cancellationToken, 
                //    includes: new Expression<Func<User, object>>[]
                //    {
                //       x => x.UserFK,
                //       x=> x.UserMembers
                //    });
				return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.User> users = await _userReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return response;
        }
    }
}