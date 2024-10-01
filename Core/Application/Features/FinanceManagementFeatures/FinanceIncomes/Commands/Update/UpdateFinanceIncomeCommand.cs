using Application.Features.FinanceManagementFeatures.FinanceIncomes.Constants;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceIncomes.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Update;

public class UpdateFinanceIncomeCommand : IRequest<UpdatedFinanceIncomeResponse>
{
    public Guid Gid { get; set; }

    public Guid GidIncomeGroupFK { get; set; }
    public Guid GidCurrencyFK { get; set; }

    public string Title { get; set; }
    public decimal Fee { get; set; }
    public DateTime MaturityDate { get; set; }
    public EnumIncomeStatus IncomeStatus { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class UpdateFinanceIncomeCommandHandler : IRequestHandler<UpdateFinanceIncomeCommand, UpdatedFinanceIncomeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceIncomeWriteRepository _financeIncomeWriteRepository;
        private readonly IFinanceIncomeReadRepository _financeIncomeReadRepository;
        private readonly FinanceIncomeBusinessRules _financeIncomeBusinessRules;

        public UpdateFinanceIncomeCommandHandler(IMapper mapper, IFinanceIncomeWriteRepository financeIncomeWriteRepository,
                                         FinanceIncomeBusinessRules financeIncomeBusinessRules, IFinanceIncomeReadRepository financeIncomeReadRepository)
        {
            _mapper = mapper;
            _financeIncomeWriteRepository = financeIncomeWriteRepository;
            _financeIncomeBusinessRules = financeIncomeBusinessRules;
            _financeIncomeReadRepository = financeIncomeReadRepository;
        }

        public async Task<UpdatedFinanceIncomeResponse> Handle(UpdateFinanceIncomeCommand request, CancellationToken cancellationToken)
        {
            X.FinanceIncome? financeIncome = await _financeIncomeReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _financeIncomeBusinessRules.FinanceIncomeShouldExistWhenSelected(financeIncome);
            await _financeIncomeBusinessRules.FinanceIncomeGroupShouldExistWhenSelected(request.GidIncomeGroupFK);
            await _financeIncomeBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            financeIncome = _mapper.Map(request, financeIncome);

            _financeIncomeWriteRepository.Update(financeIncome!);
            await _financeIncomeWriteRepository.SaveAsync();

            X.FinanceIncome updatedFinanceIncome = await _financeIncomeReadRepository.GetAsync(predicate: x => x.Gid == financeIncome.Gid, include: x => x.Include(x => x.FinanceIncomeGroupFK).Include(x => x.CurrencyFK));

            GetByGidFinanceIncomeResponse obj = _mapper.Map<GetByGidFinanceIncomeResponse>(updatedFinanceIncome);

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