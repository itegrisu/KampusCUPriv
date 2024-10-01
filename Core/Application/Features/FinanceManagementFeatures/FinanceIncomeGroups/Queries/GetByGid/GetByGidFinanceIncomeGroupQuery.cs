using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Queries.GetByGid
{
    public class GetByGidFinanceIncomeGroupQuery : IRequest<GetByGidFinanceIncomeGroupResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceIncomeGroupQueryHandler : IRequestHandler<GetByGidFinanceIncomeGroupQuery, GetByGidFinanceIncomeGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceIncomeGroupReadRepository _financeIncomeGroupReadRepository;
            private readonly FinanceIncomeGroupBusinessRules _financeIncomeGroupBusinessRules;

            public GetByGidFinanceIncomeGroupQueryHandler(IMapper mapper, IFinanceIncomeGroupReadRepository financeIncomeGroupReadRepository, FinanceIncomeGroupBusinessRules financeIncomeGroupBusinessRules)
            {
                _mapper = mapper;
                _financeIncomeGroupReadRepository = financeIncomeGroupReadRepository;
                _financeIncomeGroupBusinessRules = financeIncomeGroupBusinessRules;
            }

            public async Task<GetByGidFinanceIncomeGroupResponse> Handle(GetByGidFinanceIncomeGroupQuery request, CancellationToken cancellationToken)
            {
                X.FinanceIncomeGroup? financeIncomeGroup = await _financeIncomeGroupReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _financeIncomeGroupBusinessRules.FinanceIncomeGroupShouldExistWhenSelected(financeIncomeGroup);

                GetByGidFinanceIncomeGroupResponse response = _mapper.Map<GetByGidFinanceIncomeGroupResponse>(financeIncomeGroup);
                return response;
            }
        }
    }
}