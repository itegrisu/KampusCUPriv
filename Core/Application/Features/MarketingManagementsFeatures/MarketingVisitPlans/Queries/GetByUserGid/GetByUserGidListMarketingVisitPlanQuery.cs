using Application.Helpers.PaginationHelpers;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.MarketingManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.MarketingManagementsFeatures.MarketingVisitPlans.Queries.GetByUserGid
{
    public class GetByUserGidListMarketingVisitPlanQuery : IRequest<GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListMarketingVisitPlanQueryHandler : IRequestHandler<GetByUserGidListMarketingVisitPlanQuery, GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto>>
        {
            private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<MarketingVisitPlan, GetByUserGidListMarketingVisitPlanListItemDto> _noPagination;

            public GetByUserGidListMarketingVisitPlanQueryHandler(IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository, IMapper mapper, NoPagination<MarketingVisitPlan, GetByUserGidListMarketingVisitPlanListItemDto> noPagination)
            {
                _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto>> Handle(GetByUserGidListMarketingVisitPlanQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidPersonnelFK == request.UserGid,
                        includes: new Expression<Func<MarketingVisitPlan, object>>[]
                        {
                       x => x.UserFK,
                       x=> x.MarketingCustomerFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);

                IPaginate<MarketingVisitPlan> marketingVisitPlans = await _marketingVisitPlanReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK),
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListMarketingVisitPlanListItemDto>>(marketingVisitPlans);
                return response;
            }
        }
    }
}
