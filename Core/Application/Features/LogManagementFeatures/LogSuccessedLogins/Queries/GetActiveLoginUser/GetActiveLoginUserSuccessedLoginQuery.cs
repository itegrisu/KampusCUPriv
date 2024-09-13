using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetActiveLoginUser;

public class GetActiveLoginUserSuccessedLoginQuery : IRequest<GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetActiveLoginQuerySuccessedLoginQueryHandler : IRequestHandler<GetActiveLoginUserSuccessedLoginQuery, GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto>>
    {
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<LogSuccessedLogin, GetActiveLoginUserLogSuccessedLoginListItemDto> _noPagination;

        public GetActiveLoginQuerySuccessedLoginQueryHandler(ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, IMapper mapper, NoPagination<LogSuccessedLogin, GetActiveLoginUserLogSuccessedLoginListItemDto> noPagination)
        {
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto>> Handle(GetActiveLoginUserSuccessedLoginQuery request, CancellationToken cancellationToken)
        {

            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                   includes: new Expression<Func<LogSuccessedLogin, object>>[]
                   {
                       x => x.UserFK,
                   },
                   predicate: x => x.LogOutDate == null
                );
            }

            IPaginate<LogSuccessedLogin> logSuccessedLogins = await _logSuccessedLoginReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                predicate: x => x.LogOutDate == null
                );


            GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto> response = _mapper.Map<GetListResponse<GetActiveLoginUserLogSuccessedLoginListItemDto>>(logSuccessedLogins);
            return response;
        }
    }
}