using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.LogManagements;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetBySessionId;

public class GetBySessionIdLogUserPageVisitQuery : IRequest<GetListResponse<GetBySessionIdLogUserPageVisitListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string SessionId { get; set; }

    public class GetBySessionIdLogUserPageVisitQueryHandler : IRequestHandler<GetBySessionIdLogUserPageVisitQuery, GetListResponse<GetBySessionIdLogUserPageVisitListItemDto>>
    {
        private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogUserPageVisit, GetBySessionIdLogUserPageVisitListItemDto> _noPagination;

        public GetBySessionIdLogUserPageVisitQueryHandler(ILogUserPageVisitReadRepository logUserPageVisitReadRepository, IMapper mapper, NoPagination<X.LogUserPageVisit, GetBySessionIdLogUserPageVisitListItemDto> noPagination)
        {
            _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetBySessionIdLogUserPageVisitListItemDto>> Handle(GetBySessionIdLogUserPageVisitQuery request, CancellationToken cancellationToken)
        {
            GetListResponse<GetBySessionIdLogUserPageVisitListItemDto> response;
            IPaginate<X.LogUserPageVisit> logUserPageVisits;
           
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                       includes: new Expression<Func<LogUserPageVisit, object>>[]
                       {
                       x => x.UserFK,
                       },
                       predicate: x => x.SessionId == request.SessionId,
                       orderBy: x=> x.CreatedDate);
                }

                logUserPageVisits = await _logUserPageVisitReadRepository.GetListAllAsync(
                     index: request.PageIndex,
                     size: request.PageSize,
                     cancellationToken: cancellationToken,
                     predicate: x => x.SessionId == request.SessionId,
                     orderBy: x => x.OrderBy(x => x.CreatedDate)
                     );
            

            response = _mapper.Map<GetListResponse<GetBySessionIdLogUserPageVisitListItemDto>>(logUserPageVisits);
            return response;
        }
    }
}