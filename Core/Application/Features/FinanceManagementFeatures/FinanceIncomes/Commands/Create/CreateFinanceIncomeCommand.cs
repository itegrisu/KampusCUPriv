using Application.Features.FinanceManagementFeatures.FinanceIncomes.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Create;

public class CreateFinanceIncomeCommand : IRequest<CreatedFinanceIncomeResponse>
{
    public Guid GidIncomeGroupFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string Title { get; set; }
    public decimal Fee { get; set; }
    public DateTime MaturityDate { get; set; }
    public EnumIncomeStatus IncomeStatus { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class CreateFinanceIncomeCommandHandler : IRequestHandler<CreateFinanceIncomeCommand, CreatedFinanceIncomeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeWriteRepository _financeIncomeWriteRepository;
        private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
        private readonly FinanceIncomeBusinessRules _financeIncomeBusinessRules;

        public CreateFinanceIncomeCommandHandler(IMapper mapper, IFinanceIncomeWriteRepository financeIncomeWriteRepository,
                                         FinanceIncomeBusinessRules financeIncomeBusinessRules, IFinanceIncomeReadRepository financeIncomeReadRepository)
        {
            _mapper = mapper;
            _financeIncomeWriteRepository = financeIncomeWriteRepository;
            _financeIncomeBusinessRules = financeIncomeBusinessRules;
            _financeIncomeReadRepository = financeIncomeReadRepository;
        }

        public async Task<CreatedFinanceIncomeResponse> Handle(CreateFinanceIncomeCommand request, CancellationToken cancellationToken)
        {
            await _financeIncomeBusinessRules.FinanceIncomeGroupShouldExistWhenSelected(request.GidIncomeGroupFK);
            await _financeIncomeBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);

            X.FinanceIncome financeIncome = _mapper.Map<X.FinanceIncome>(request);


            await _financeIncomeWriteRepository.AddAsync(financeIncome);
            await _financeIncomeWriteRepository.SaveAsync();

            X.FinanceIncome savedFinanceIncome = await _financeIncomeReadRepository.GetAsync(predicate: x => x.Gid == financeIncome.Gid, include: x => x.Include(x => x.FinanceIncomeGroupFK).Include(x => x.CurrencyFK));


            GetByGidFinanceIncomeResponse obj = _mapper.Map<GetByGidFinanceIncomeResponse>(savedFinanceIncome);
            return new()
            {
                Title = FinanceIncomesBusinessMessages.ProcessCompleted,
                Message = FinanceIncomesBusinessMessages.SuccessCreatedFinanceIncomeMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}