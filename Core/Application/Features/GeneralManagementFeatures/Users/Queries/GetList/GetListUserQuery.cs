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

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<User, GetListUserListItemDto> _noPagination;

        public GetListUserQueryHandler(IUserReadRepository userReadRepository, IMapper mapper, NoPagination<User, GetListUserListItemDto> noPagination)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(
                    cancellationToken,
                    includes: new Expression<Func<User, object>>[]
                    {
                       x => x.CountryFK,

                    });
            }

            IPaginate<User> userCustoms = await _userReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                 include: i => i.Include(i => i.CountryFK)
            );


            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(userCustoms);
            return response;
        }
    }
}