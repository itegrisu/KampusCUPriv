using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Queries.GetByGid
{
    public class GetByGidFinanceExpenseGroupQuery : IRequest<GetByGidFinanceExpenseGroupResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceExpenseGroupQueryHandler : IRequestHandler<GetByGidFinanceExpenseGroupQuery, GetByGidFinanceExpenseGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceExpenseGroupReadRepository _financeExpenseGroupReadRepository;
            private readonly FinanceExpenseGroupBusinessRules _financeExpenseGroupBusinessRules;

            public GetByGidFinanceExpenseGroupQueryHandler(IMapper mapper, IFinanceExpenseGroupReadRepository financeExpenseGroupReadRepository, FinanceExpenseGroupBusinessRules financeExpenseGroupBusinessRules)
            {
                _mapper = mapper;
                _financeExpenseGroupReadRepository = financeExpenseGroupReadRepository;
                _financeExpenseGroupBusinessRules = financeExpenseGroupBusinessRules;
            }

            public async Task<GetByGidFinanceExpenseGroupResponse> Handle(GetByGidFinanceExpenseGroupQuery request, CancellationToken cancellationToken)
            {
                X.FinanceExpenseGroup? financeExpenseGroup = await _financeExpenseGroupReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                await _financeExpenseGroupBusinessRules.FinanceExpenseGroupShouldExistWhenSelected(financeExpenseGroup);

                GetByGidFinanceExpenseGroupResponse response = _mapper.Map<GetByGidFinanceExpenseGroupResponse>(financeExpenseGroup);
                return response;
            }
        }
    }
}