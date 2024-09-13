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

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetList;

public class GetListLogUserPageVisitQuery : IRequest<GetListResponse<GetListLogUserPageVisitListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? UserGid { get; set; }

    public class GetListLogUserPageVisitQueryHandler : IRequestHandler<GetListLogUserPageVisitQuery, GetListResponse<GetListLogUserPageVisitListItemDto>>
    {
        private readonly ILogUserPageVisitReadRepository _logUserPageVisitReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogUserPageVisit, GetListLogUserPageVisitListItemDto> _noPagination;

        public GetListLogUserPageVisitQueryHandler(ILogUserPageVisitReadRepository logUserPageVisitReadRepository, IMapper mapper, NoPagination<X.LogUserPageVisit, GetListLogUserPageVisitListItemDto> noPagination)
        {
            _logUserPageVisitReadRepository = logUserPageVisitReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogUserPageVisitListItemDto>> Handle(GetListLogUserPageVisitQuery request, CancellationToken cancellationToken)
        {
            GetListResponse<GetListLogUserPageVisitListItemDto> response;
            IPaginate<X.LogUserPageVisit> logUserPageVisits;
            if (request.UserGid == null || request.UserGid == "0")
            {
                if (request.PageIndex == -1)
                {
                    //return await _noPagination.NoPaginationData(cancellationToken,
                    //   includes: new Expression<Func<LogUserPageVisit, object>>[]
                    //   {
                    //   x => x.UserFK,
                    //   },
                    //   predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);

                    request.PageSize = 1000;
                    request.PageIndex = 0;
                }

                logUserPageVisits = await _logUserPageVisitReadRepository.GetListAllAsync(
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
                    //return await _noPagination.NoPaginationData(cancellationToken,
                    //            includes: new Expression<Func<LogUserPageVisit, object>>[]
                    //   {
                    //   x => x.UserFK,
                    //   },
                    //   predicate: x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid);
                    request.PageSize = 1000;
                    request.PageIndex = 0;
                }

                logUserPageVisits = await _logUserPageVisitReadRepository.GetListAllAsync(
                                        index: request.PageIndex,
                                        size: request.PageSize,
                                        cancellationToken: cancellationToken,
                                         predicate: x => x.GidUserFK.ToString() == request.UserGid && x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime);

            }

            response = _mapper.Map<GetListResponse<GetListLogUserPageVisitListItemDto>>(logUserPageVisits);
            response.Items = response.Items
              .GroupBy(item => item.PageInfo)
                 .Select(group => group.First())
                  .ToList();
          

            return response;
        }
    }
}