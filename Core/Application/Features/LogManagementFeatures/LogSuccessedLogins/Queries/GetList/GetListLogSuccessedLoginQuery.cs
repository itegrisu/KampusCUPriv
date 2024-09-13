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

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetList;

public class GetListLogSuccessedLoginQuery : IRequest<GetListResponse<GetListLogSuccessedLoginListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? UserGid { get; set; }

    public class GetListLogSuccessedLoginQueryHandler : IRequestHandler<GetListLogSuccessedLoginQuery, GetListResponse<GetListLogSuccessedLoginListItemDto>>
    {
        private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogSuccessedLogin, GetListLogSuccessedLoginListItemDto> _noPagination;

        public GetListLogSuccessedLoginQueryHandler(ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, IMapper mapper, NoPagination<X.LogSuccessedLogin, GetListLogSuccessedLoginListItemDto> noPagination)
        {
            _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogSuccessedLoginListItemDto>> Handle(GetListLogSuccessedLoginQuery request, CancellationToken cancellationToken)
        {
            IPaginate<X.LogSuccessedLogin> logSuccessedLogins;
            if (request.UserGid == null || request.UserGid == "0")
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       includes: new Expression<Func<LogSuccessedLogin, object>>[]
                       {
                       x => x.UserFK,
                       },
                       predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);
                       
                }
                logSuccessedLogins = await _logSuccessedLoginReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime
            );
            }
            else
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       includes: new Expression<Func<LogSuccessedLogin, object>>[]
                       {
                       x => x.UserFK,
                       },
                       predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid);

                }
                logSuccessedLogins = await _logSuccessedLoginReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid
            );
            }

            GetListResponse<GetListLogSuccessedLoginListItemDto> response = _mapper.Map<GetListResponse<GetListLogSuccessedLoginListItemDto>>(logSuccessedLogins);
            return response;
        }
    }
}