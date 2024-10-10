using Application.Features.MarketingManagementFeatures.MarketingCustomers.Rules;
using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.MarketingManagements;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Queries.GetByGid
{
    public class GetByGidMarketingCustomerQuery : IRequest<GetByGidMarketingCustomerResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidMarketingCustomerQueryHandler : IRequestHandler<GetByGidMarketingCustomerQuery, GetByGidMarketingCustomerResponse>
        {
            private readonly IMapper _mapper;
            private readonly IMarketingCustomerReadRepository _marketingCustomerReadRepository;
            private readonly MarketingCustomerBusinessRules _marketingCustomerBusinessRules;

            public GetByGidMarketingCustomerQueryHandler(IMapper mapper, IMarketingCustomerReadRepository marketingCustomerReadRepository, MarketingCustomerBusinessRules marketingCustomerBusinessRules)
            {
                _mapper = mapper;
                _marketingCustomerReadRepository = marketingCustomerReadRepository;
                _marketingCustomerBusinessRules = marketingCustomerBusinessRules;
            }

            public async Task<GetByGidMarketingCustomerResponse> Handle(GetByGidMarketingCustomerQuery request, CancellationToken cancellationToken)
            {
                X.MarketingCustomer? marketingCustomer = await _marketingCustomerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _marketingCustomerBusinessRules.MarketingCustomerShouldExistWhenSelected(marketingCustomer);

                GetByGidMarketingCustomerResponse response = _mapper.Map<GetByGidMarketingCustomerResponse>(marketingCustomer);
                return response;
            }
        }
    }
}