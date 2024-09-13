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
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Rules;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetLastTenLogin;

public class GetLastTenLogSuccessedLoginQuery : IRequest<GetListResponse<GetLastTenLogSuccessedLoginListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public Guid UserGid { get; set; }

    public class GetLastTenLogSuccessedLoginQueryHandler : IRequestHandler<GetLastTenLogSuccessedLoginQuery, GetListResponse<GetLastTenLogSuccessedLoginListItemDto>>
    {
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<LogSuccessedLogin, GetLastTenLogSuccessedLoginListItemDto> _noPagination;
        private readonly LogSuccessedLoginBusinessRules _logSuccessedLoginBusinessRules;

        public GetLastTenLogSuccessedLoginQueryHandler(ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, IMapper mapper, NoPagination<LogSuccessedLogin, GetLastTenLogSuccessedLoginListItemDto> noPagination, LogSuccessedLoginBusinessRules logSuccessedLoginBusinessRules)
        {
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
            _logSuccessedLoginBusinessRules = logSuccessedLoginBusinessRules;
        }

        public async Task<GetListResponse<GetLastTenLogSuccessedLoginListItemDto>> Handle(GetLastTenLogSuccessedLoginQuery request, CancellationToken cancellationToken)
        {
            //IPaginate<X.LogSuccessedLogin> logSuccessedLogins;
            //if (request.UserGid == null || request.UserGid == "0")
            //{
            //    if (request.PageIndex == -1)
            //    {
            //        return await _noPagination.NoPaginationData(cancellationToken,
            //           includes: new Expression<Func<LogSuccessedLogin, object>>[]
            //           {
            //           x => x.UserFK,
            //           },
            //           predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);

            //    }

            await _logSuccessedLoginBusinessRules.UserShouldExistWhenSelected(request.UserGid);

            IPaginate<LogSuccessedLogin> logSuccessedLogins = await _logSuccessedLoginReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                include: X => X.Include(x => x.UserFK),
                predicate: x => x.GidUserFK == request.UserGid,
                orderBy: x => x.OrderByDescending(x => x.CreatedDate)
            );

            GetListResponse<GetLastTenLogSuccessedLoginListItemDto> response = _mapper.Map<GetListResponse<GetLastTenLogSuccessedLoginListItemDto>>(logSuccessedLogins);
            return response;
        }
    }
}