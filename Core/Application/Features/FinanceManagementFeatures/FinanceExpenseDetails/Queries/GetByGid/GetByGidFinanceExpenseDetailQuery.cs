using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Queries.GetByGid
{
    public class GetByGidFinanceExpenseDetailQuery : IRequest<GetByGidFinanceExpenseDetailResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceExpenseDetailQueryHandler : IRequestHandler<GetByGidFinanceExpenseDetailQuery, GetByGidFinanceExpenseDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceExpenseDetailReadRepository _financeExpenseDetailReadRepository;
            private readonly FinanceExpenseDetailBusinessRules _financeExpenseDetailBusinessRules;

            public GetByGidFinanceExpenseDetailQueryHandler(IMapper mapper, IFinanceExpenseDetailReadRepository financeExpenseDetailReadRepository, FinanceExpenseDetailBusinessRules financeExpenseDetailBusinessRules)
            {
                _mapper = mapper;
                _financeExpenseDetailReadRepository = financeExpenseDetailReadRepository;
                _financeExpenseDetailBusinessRules = financeExpenseDetailBusinessRules;
            }

            public async Task<GetByGidFinanceExpenseDetailResponse> Handle(GetByGidFinanceExpenseDetailQuery request, CancellationToken cancellationToken)
            {
                X.FinanceExpenseDetail? financeExpenseDetail = await _financeExpenseDetailReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ControlPersonnelFK).Include(x => x.CurrencyFK).Include(x => x.FinanceExpenseFK).Include(x => x.SpendPersonnelFK));

                await _financeExpenseDetailBusinessRules.FinanceExpenseDetailShouldExistWhenSelected(financeExpenseDetail);

                GetByGidFinanceExpenseDetailResponse response = _mapper.Map<GetByGidFinanceExpenseDetailResponse>(financeExpenseDetail);
                return response;
            }
        }
    }
}