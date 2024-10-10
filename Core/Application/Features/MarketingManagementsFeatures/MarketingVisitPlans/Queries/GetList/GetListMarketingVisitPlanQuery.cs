using Application.Helpers.PaginationHelpers;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.MarketingManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetList;

public class GetListMarketingVisitPlanQuery : IRequest<GetListResponse<GetListMarketingVisitPlanListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListMarketingVisitPlanQueryHandler : IRequestHandler<GetListMarketingVisitPlanQuery, GetListResponse<GetListMarketingVisitPlanListItemDto>>
    {
        private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.MarketingVisitPlan, GetListMarketingVisitPlanListItemDto> _noPagination;

        public GetListMarketingVisitPlanQueryHandler(IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository, IMapper mapper, NoPagination<X.MarketingVisitPlan, GetListMarketingVisitPlanListItemDto> noPagination)
        {
            _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListMarketingVisitPlanListItemDto>> Handle(GetListMarketingVisitPlanQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<MarketingVisitPlan, object>>[]
                    {
                       x => x.UserFK,
                       x=> x.MarketingCustomerFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);

            IPaginate<X.MarketingVisitPlan> marketingVisitPlans = await _marketingVisitPlanReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMarketingVisitPlanListItemDto> response = _mapper.Map<GetListResponse<GetListMarketingVisitPlanListItemDto>>(marketingVisitPlans);
            return response;
        }
    }
}