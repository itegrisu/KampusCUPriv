using Application.Helpers.PaginationHelpers;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.MarketingManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByYearList
{
    public class GetByYearListMarketingVisitPlanQuery : IRequest<GetListResponse<GetByYearListMarketingVisitPlanListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int Year { get; set; }
        public class GetByYearListMarketingVisitPlanQueryHandler : IRequestHandler<GetByYearListMarketingVisitPlanQuery, GetListResponse<GetByYearListMarketingVisitPlanListItemDto>>
        {
            private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<MarketingVisitPlan, GetByYearListMarketingVisitPlanListItemDto> _noPagination;

            public GetByYearListMarketingVisitPlanQueryHandler(IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository, IMapper mapper, NoPagination<MarketingVisitPlan, GetByYearListMarketingVisitPlanListItemDto> noPagination)
            {
                _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByYearListMarketingVisitPlanListItemDto>> Handle(GetByYearListMarketingVisitPlanQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.PlanningVisitDate.Year == request.Year,
                        includes: new Expression<Func<MarketingVisitPlan, object>>[]
                        {
                           x => x.UserFK,
                           x => x.MarketingCustomerFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);

                IPaginate<MarketingVisitPlan> marketingVisitPlans = await _marketingVisitPlanReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK),
                    predicate: x => x.PlanningVisitDate.Year == request.Year,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByYearListMarketingVisitPlanListItemDto> response = _mapper.Map<GetListResponse<GetByYearListMarketingVisitPlanListItemDto>>(marketingVisitPlans);
                return response;
            }
        }
    }
}
