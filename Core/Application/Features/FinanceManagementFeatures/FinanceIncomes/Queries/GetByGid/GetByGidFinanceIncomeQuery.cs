using Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid
{
    public class GetByGidFinanceIncomeQuery : IRequest<GetByGidFinanceIncomeResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceIncomeQueryHandler : IRequestHandler<GetByGidFinanceIncomeQuery, GetByGidFinanceIncomeResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
            private readonly FinanceIncomeBusinessRules _financeIncomeBusinessRules;

            public GetByGidFinanceIncomeQueryHandler(IMapper mapper, IFinanceIncomeReadRepository financeIncomeReadRepository, FinanceIncomeBusinessRules financeIncomeBusinessRules)
            {
                _mapper = mapper;
                _financeIncomeReadRepository = financeIncomeReadRepository;
                _financeIncomeBusinessRules = financeIncomeBusinessRules;
            }

            public async Task<GetByGidFinanceIncomeResponse> Handle(GetByGidFinanceIncomeQuery request, CancellationToken cancellationToken)
            {
                X.FinanceIncome? financeIncome = await _financeIncomeReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.FinanceIncomeGroupFK).Include(x => x.CurrencyFK));

                await _financeIncomeBusinessRules.FinanceIncomeShouldExistWhenSelected(financeIncome);

                GetByGidFinanceIncomeResponse response = _mapper.Map<GetByGidFinanceIncomeResponse>(financeIncome);
                return response;
            }
        }
    }
}