using Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Rules;
using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Queries.GetByGid
{
    public class GetByGidMarketingVisitPlanQuery : IRequest<GetByGidMarketingVisitPlanResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidMarketingVisitPlanQueryHandler : IRequestHandler<GetByGidMarketingVisitPlanQuery, GetByGidMarketingVisitPlanResponse>
        {
            private readonly IMapper _mapper;
            private readonly IMarketingVisitPlanReadRepository _marketingVisitPlanReadRepository;
            private readonly MarketingVisitPlanBusinessRules _marketingVisitPlanBusinessRules;

            public GetByGidMarketingVisitPlanQueryHandler(IMapper mapper, IMarketingVisitPlanReadRepository marketingVisitPlanReadRepository, MarketingVisitPlanBusinessRules marketingVisitPlanBusinessRules)
            {
                _mapper = mapper;
                _marketingVisitPlanReadRepository = marketingVisitPlanReadRepository;
                _marketingVisitPlanBusinessRules = marketingVisitPlanBusinessRules;
            }

            public async Task<GetByGidMarketingVisitPlanResponse> Handle(GetByGidMarketingVisitPlanQuery request, CancellationToken cancellationToken)
            {
                X.MarketingVisitPlan? marketingVisitPlan = await _marketingVisitPlanReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, include: x => x.Include(x => x.MarketingCustomerFK).Include(x => x.UserFK), cancellationToken: cancellationToken);
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _marketingVisitPlanBusinessRules.MarketingVisitPlanShouldExistWhenSelected(marketingVisitPlan);

                GetByGidMarketingVisitPlanResponse response = _mapper.Map<GetByGidMarketingVisitPlanResponse>(marketingVisitPlan);
                return response;
            }
        }
    }
}