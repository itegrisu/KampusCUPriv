using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetList;

public class GetListLogAuthorizationErrorQuery : IRequest<GetListResponse<GetListLogAuthorizationErrorListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string UserGid { get; set; }

    public class GetListLogAuthorizationErrorQueryHandler : IRequestHandler<GetListLogAuthorizationErrorQuery, GetListResponse<GetListLogAuthorizationErrorListItemDto>>
    {
        private readonly ILogAuthorizationErrorReadRepository _logAuthorizationErrorReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogAuthorizationError, GetListLogAuthorizationErrorListItemDto> _noPagination;

        public GetListLogAuthorizationErrorQueryHandler(ILogAuthorizationErrorReadRepository logAuthorizationErrorReadRepository, IMapper mapper, NoPagination<X.LogAuthorizationError, GetListLogAuthorizationErrorListItemDto> noPagination)
        {
            _logAuthorizationErrorReadRepository = logAuthorizationErrorReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogAuthorizationErrorListItemDto>> Handle(GetListLogAuthorizationErrorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<X.LogAuthorizationError> logAuthorizationErrors;
            GetListResponse<GetListLogAuthorizationErrorListItemDto> response;
            if (request.UserGid == null || request.UserGid == "0")
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        includes: new Expression<Func<LogAuthorizationError, object>>[]
                        {
                            x => x.UserFK,
                        },
                        predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);
                }

                logAuthorizationErrors = await _logAuthorizationErrorReadRepository.GetListAllAsync(
               index: request.PageIndex,
               size: request.PageSize,
               cancellationToken: cancellationToken,
               include: X => X.Include(x => x.UserFK),
               predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime
               );
              
                response = _mapper.Map<GetListResponse<GetListLogAuthorizationErrorListItemDto>>(logAuthorizationErrors);


            }
            else
            {
                if (request.PageIndex == -1)
                    return await _noPagination.NoPaginationData(cancellationToken,
                        includes: new Expression<Func<LogAuthorizationError, object>>[]
                        {
                       x => x.UserFK,
                        },
                        predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid);
                

                  logAuthorizationErrors = await _logAuthorizationErrorReadRepository.GetListAllAsync(
                     index: request.PageIndex,
                     size: request.PageSize,
                     cancellationToken: cancellationToken,
                     include: X => X.Include(x => x.UserFK),
                     predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid);
                     response = _mapper.Map<GetListResponse<GetListLogAuthorizationErrorListItemDto>>(logAuthorizationErrors);
               
            }
            return response;
        }
    }
}