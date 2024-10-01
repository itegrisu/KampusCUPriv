using Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid
{
    public class GetByGidFinanceExpenseQuery : IRequest<GetByGidFinanceExpenseResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceExpenseQueryHandler : IRequestHandler<GetByGidFinanceExpenseQuery, GetByGidFinanceExpenseResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
            private readonly FinanceExpenseBusinessRules _financeExpenseBusinessRules;

            public GetByGidFinanceExpenseQueryHandler(IMapper mapper, IFinanceExpenseReadRepository financeExpenseReadRepository, FinanceExpenseBusinessRules financeExpenseBusinessRules)
            {
                _mapper = mapper;
                _financeExpenseReadRepository = financeExpenseReadRepository;
                _financeExpenseBusinessRules = financeExpenseBusinessRules;
            }

            public async Task<GetByGidFinanceExpenseResponse> Handle(GetByGidFinanceExpenseQuery request, CancellationToken cancellationToken)
            {
                X.FinanceExpense? financeExpense = await _financeExpenseReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.FinanceExpenseGroupFK).Include(x => x.CurrencyFK).Include(x => x.MoneySenderPersonnelFK).Include(x => x.ApprovalReceiverFK).Include(x => x.OrganizationFK));
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _financeExpenseBusinessRules.FinanceExpenseShouldExistWhenSelected(financeExpense);

                GetByGidFinanceExpenseResponse response = _mapper.Map<GetByGidFinanceExpenseResponse>(financeExpense);
                return response;
            }
        }
    }
}