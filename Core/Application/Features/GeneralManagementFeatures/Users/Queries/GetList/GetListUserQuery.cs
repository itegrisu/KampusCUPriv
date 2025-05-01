using Application.Helpers.PaginationHelpers;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using System.Linq.Expressions;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;

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
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<User, object>>[]
                    {
                       x => x.ClassFK,
                       x => x.DepartmentFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.User> users = await _userReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.ClassFK).Include(x => x.DepartmentFK)
            );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return response;
        }
    }
}