using Application.Features.FinanceManagementFeatures.FinanceExpenses.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceExpenses.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.FinanceManagements;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Update;

public class UpdateFinanceExpenseCommand : IRequest<UpdatedFinanceExpenseResponse>
{
    public Guid Gid { get; set; }

    public Guid GidExpenseGroupFK { get; set; }
    public string? GidOrganizationFK { get; set; }
    public Guid GidMoneySenderPersonnelFK { get; set; }
    public Guid GidMoneyReceivePersonnelFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public string? GidApprovalReceiverFK { get; set; }

    public string Title { get; set; }
    public decimal AmountSpent { get; set; }
    public DateTime TransactionDate { get; set; }
    public EnumExpenseStatus ExpenseStatus { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }
    public EnumReceiverAcceptStatus ReceiverAcceptStatus { get; set; }
    public DateTime? ReceiverAcceptDate { get; set; }
    public DateTime? ReceiverRejectDate { get; set; }
    public string? ReceiverIpAddress { get; set; }



    public class UpdateFinanceExpenseCommandHandler : IRequestHandler<UpdateFinanceExpenseCommand, UpdatedFinanceExpenseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceExpenseWriteRepository _financeExpenseWriteRepository;
        private readonly IFinanceExpenseReadRepository _financeExpenseReadRepository;
        private readonly FinanceExpenseBusinessRules _financeExpenseBusinessRules;

        public UpdateFinanceExpenseCommandHandler(IMapper mapper, IFinanceExpenseWriteRepository financeExpenseWriteRepository,
                                         FinanceExpenseBusinessRules financeExpenseBusinessRules, IFinanceExpenseReadRepository financeExpenseReadRepository)
        {
            _mapper = mapper;
            _financeExpenseWriteRepository = financeExpenseWriteRepository;
            _financeExpenseBusinessRules = financeExpenseBusinessRules;
            _financeExpenseReadRepository = financeExpenseReadRepository;
        }

        public async Task<UpdatedFinanceExpenseResponse> Handle(UpdateFinanceExpenseCommand request, CancellationToken cancellationToken)
        {
            X.FinanceExpense? financeExpense = await _financeExpenseReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _financeExpenseBusinessRules.FinanceExpenseShouldExistWhenSelected(financeExpense);
            await _financeExpenseBusinessRules.FinanceExpenseGroupShouldExistWhenSelected(request.GidExpenseGroupFK);
            await _financeExpenseBusinessRules.OrganizationShouldExistWhenSelected(request.GidOrganizationFK);
            await _financeExpenseBusinessRules.MoneySenderPersonnelShouldExistWhenSelected(request.GidMoneySenderPersonnelFK);
            await _financeExpenseBusinessRules.MoneyReceivePersonnelShouldExistWhenSelected(request.GidMoneyReceivePersonnelFK);
            await _financeExpenseBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            await _financeExpenseBusinessRules.ApprovalReceiverShouldExistWhenSelected(request.GidApprovalReceiverFK);

            financeExpense = _mapper.Map(request, financeExpense);

            _financeExpenseWriteRepository.Update(financeExpense!);
            await _financeExpenseWriteRepository.SaveAsync();

            X.FinanceExpense updatedFinanceExpense = await _financeExpenseReadRepository.GetAsync(predicate: x => x.Gid == financeExpense.Gid, include: x => x.Include(x => x.FinanceExpenseGroupFK).Include(x => x.CurrencyFK).Include(x => x.MoneySenderPersonnelFK).Include(x => x.ApprovalReceiverFK).Include(x => x.OrganizationFK));

            GetByGidFinanceExpenseResponse obj = _mapper.Map<GetByGidFinanceExpenseResponse>(updatedFinanceExpense);

            return new()
            {
                Title = FinanceExpensesBusinessMessages.ProcessCompleted,
                Message = FinanceExpensesBusinessMessages.SuccessCreatedFinanceExpenseMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}