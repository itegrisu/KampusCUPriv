using Application.Helpers.PaginationHelpers;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.LogManagements;
using MediatR;
using System.Linq.Expressions;
using Domain.Entities.LogManagements;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetList;

public class GetListLogUserPageVisitActionQuery : IRequest<GetListResponse<GetListLogUserPageVisitActionListItemDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? UserGid { get; set; }
    public string? PageInfo { get; set; }


    public class GetListLogUserPageVisitActionQueryHandler : IRequestHandler<GetListLogUserPageVisitActionQuery, GetListResponse<GetListLogUserPageVisitActionListItemDto>>
    {
        private readonly ILogUserPageVisitActionReadRepository _logUserPageVisitActionReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.LogUserPageVisitAction, GetListLogUserPageVisitActionListItemDto> _noPagination;

        public GetListLogUserPageVisitActionQueryHandler(ILogUserPageVisitActionReadRepository logUserPageVisitActionReadRepository, IMapper mapper, NoPagination<X.LogUserPageVisitAction, GetListLogUserPageVisitActionListItemDto> noPagination)
        {
            _logUserPageVisitActionReadRepository = logUserPageVisitActionReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListLogUserPageVisitActionListItemDto>> Handle(GetListLogUserPageVisitActionQuery request, CancellationToken cancellationToken)
        {
            

            IPaginate<LogUserPageVisitAction> logUserPageVisitActions;
            GetListResponse<GetListLogUserPageVisitActionListItemDto> response;

            Expression<Func<LogUserPageVisitAction, bool>> predicate;
            if (request.UserGid == null || request.UserGid == "0")
            {
                
                if (request.PageInfo == "0" || request.PageInfo ==null)
                {
                    predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime;
                }
                else
                {
                    predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.PageInfo== request.PageInfo; // þimdilik içeriðe bakýyoruz bunu tamamen karþýlaþtýrmalý yapabiliriz
                }
            }
            else
            {
              
                if (request.PageInfo == "0" || request.PageInfo == null)
                {
                    predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid;
                }
                else
                {
                    predicate = x => x.CreatedDate >= request.StartTime && x.CreatedDate <= request.EndTime && x.GidUserFK.ToString() == request.UserGid && x.PageInfo==request.PageInfo;
                }
            }

            // redirectName kontrolü
           

            if (request.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(
                    cancellationToken,
                    includes: new Expression<Func<LogUserPageVisitAction, object>>[]
                    {
            x => x.UserFK,
                    },
                    predicate: predicate);
            }

            logUserPageVisitActions = await _logUserPageVisitActionReadRepository.GetListAllAsync(
                index: request.PageIndex,
                size: request.PageSize,
                cancellationToken: cancellationToken,
                include: X => X.Include(x => x.UserFK),
                predicate: predicate);

            response = _mapper.Map<GetListResponse<GetListLogUserPageVisitActionListItemDto>>(logUserPageVisitActions);
            return response;

        }
    }
}