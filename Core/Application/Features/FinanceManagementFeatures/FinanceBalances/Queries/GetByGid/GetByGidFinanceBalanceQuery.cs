using AutoMapper;
using MediatR;
using X = Domain.Entities.FinanceManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid
{
    public class GetByGidFinanceBalanceQuery : IRequest<GetByGidFinanceBalanceResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidFinanceBalanceQueryHandler : IRequestHandler<GetByGidFinanceBalanceQuery, GetByGidFinanceBalanceResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
            private readonly FinanceBalanceBusinessRules _financeBalanceBusinessRules;

            public GetByGidFinanceBalanceQueryHandler(IMapper mapper, IFinanceBalanceReadRepository financeBalanceReadRepository, FinanceBalanceBusinessRules financeBalanceBusinessRules)
            {
                _mapper = mapper;
                _financeBalanceReadRepository = financeBalanceReadRepository;
                _financeBalanceBusinessRules = financeBalanceBusinessRules;
            }

            public async Task<GetByGidFinanceBalanceResponse> Handle(GetByGidFinanceBalanceQuery request, CancellationToken cancellationToken)
            {
                X.FinanceBalance? financeBalance = await _financeBalanceReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken,
                      include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _financeBalanceBusinessRules.FinanceBalanceShouldExistWhenSelected(financeBalance);

                GetByGidFinanceBalanceResponse response = _mapper.Map<GetByGidFinanceBalanceResponse>(financeBalance);
                return response;
            }
        }
    }
}